using Ecommerce.Utilities.EFCore.Attributes;
using Ecommerce.Utilities.EFCore.CommandGenerators;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Ecommerce.Utilities.EFCore
{
    public abstract class SqlCommandBase : ISqlCommandBase
    {
        private string? _sql;
        private CommandType? _commandType;
        private SqlParameter[]? _parameters;

        public virtual string? Sql
        {
            get => _sql ??= GetSql();
            set => _sql = value;
        }

        public virtual CommandType CommandType
        {
            get
            {
                if (!_commandType.HasValue)
                    _commandType = GetCommandType();
                return _commandType.Value;
            }
            set => _commandType = value;
        }

        public virtual SqlParameter[] Parameters
        {
            get => _parameters ??= GetSqlParameters();
            set => _parameters = value;
        }

        private string? GetSql()
        {
            var commandAttr = GetType().GetCustomAttribute<SqlCommandAttribute>();
            return commandAttr?.Sql;
        }

        public T? OutputValue<T>(string outputProperty)
        {
            var value = OutputValue(outputProperty);
            if (value != null)
                return (T)value;

            return default;
        }

        public object? OutputValue(string outputProperty)
        {
            if (Parameters?.Any() == true)
            {
                var property = GetType().GetProperty(outputProperty);
                if (property != null)
                {
                    LoadOutputParameters();
                    return property?.GetValue(this);
                }
            }
            return default;
        }

        public void LoadOutputParameters()
        {
            if (Parameters?.Any() == true)
            {
                var outputProps = GetType()
                    .GetProperties()
                    .Where(p => p.CanWrite)
                    .Select(p => new
                    {
                        Property = p,
                        Attribute = p.GetCustomAttribute<SqlParameterAttribute>()
                    })
                    .Where(p =>
                        p.Attribute != null
                        && p.Attribute.ParameterDirection == ParameterDirection.Output
                    );

                if (outputProps.Any())
                {
                    foreach (var prop in outputProps)
                    {
                        var parameter = Parameters?.
                            FirstOrDefault(p => p.ParameterName == prop.Attribute?.ParameterName);

                        if (parameter?.Value != null)
                            prop.Property.SetValue(this, parameter.Value);
                    }
                }
            }
        }

        private CommandType GetCommandType()
        {
            var commandType = GetType().GetCustomAttribute<SqlCommandAttribute>();
            return commandType?.CommandType ?? CommandType.Text;
        }

        private SqlParameter[] GetSqlParameters()
        {
            var paramaters = GetType()
                .GetProperties()
                .Where(p => p.CanWrite)
                .Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttribute<SqlParameterAttribute>()
                })
                .Where(p => p.Attribute != null);

            if (paramaters.Any())
            {
                return paramaters
                    .Select(p =>
                    {
                        // Parameter value
                        var value = p.Property.GetValue(this);
                        // Parameter type
                        var parameterType = p.Attribute?.GetParameterType();
                        // Parameter type name. For the case using user defined type
                        var parameterTypeName = p.Attribute?.GetParameterTypeName();

                        // Ignore if user defined type and have no record TODO: Need to confirm
                        if (parameterType is SqlDbType.Structured)
                        {
                            bool hasValue = false;
                            if (value is IEnumerable variable)
                            {
                                foreach (var item in variable)
                                {
                                    hasValue = true;
                                    break;
                                }
                                if (!hasValue) return null;
                            }
                            return null;
                        }

                        var parameter = new SqlParameter(p.Attribute?.ParameterName, value ?? DBNull.Value)
                        {
                            Direction = p.Attribute?.ParameterDirection ?? ParameterDirection.Input
                        };

                        if (parameterType.HasValue)
                        {
                            parameter.SqlDbType = parameterType.Value;
                        }

                        if (!string.IsNullOrEmpty(parameterTypeName))
                        {
                            parameter.TypeName = parameterTypeName;
                        }

                        return parameter;
                    })
                    .OfType<SqlParameter>()
                    .ToArray();
            }

            return new SqlParameter[] { };
        }
    }
}