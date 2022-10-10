using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecommerce.Utilities.EFCore.Extensions
{
    public static class DatabaseExtensions
    {
        public static List<TEntity> ToList<TEntity>(this DataTable source) where TEntity : new()
        {
            var result = new List<TEntity>();

            var props = typeof(TEntity)
                .GetProperties()
                .Where(p => p.CanWrite && source.Columns.Contains(p.Name))
                .ToList();

            foreach (DataRow row in source.Rows)
            {
                var dataRow = new TEntity();
                foreach (var p in props)
                {
                    var colVal = row[p.Name];
                    if (!Convert.IsDBNull(colVal)) p.SetValue(dataRow, colVal);
                }
                result.Add(dataRow);
            }

            return result;
        }

        public static object? ToList(this DataTable source, Type collectionType)
        {
            if (!collectionType.IsGenericType) return null;

            var result = new List<object?>();

            var genericType = collectionType.GenericTypeArguments[0];

            var props = genericType
                .GetProperties()
                .Where(p => p.CanWrite && source.Columns.Contains(p.Name))
                .ToList();

            foreach (DataRow row in source.Rows)
            {
                var dataRow = Activator.CreateInstance(genericType);
                foreach (var p in props)
                {
                    var colVal = row[p.Name];
                    if (!Convert.IsDBNull(colVal)) p.SetValue(dataRow, colVal);
                }
                result.Add(dataRow);
            }

            return JsonSerializer.Serialize(JsonSerializer.Serialize(result), collectionType);
        }

        public static object? GetKeyValue(this EntityEntry entry)
        {
            var props = entry.Metadata.FindPrimaryKey()?.Properties;
            if (props?.Any() == true)
            {
                if (props.Count == 1) return props[0].GetValue(entry);

                var jObject = new JObject();
                foreach (var prop in props)
                {
                    jObject.Add(prop.Name, prop.GetValue(entry)?.ToString());
                }

                return jObject.ToString();
            }
            return default;
        }

        public static object? GetValue(this IProperty key, EntityEntry entry)
        {
            var value = entry?.Entity?.GetType()?
                .GetProperties()?.Where(x => x.Name == key.Name).FirstOrDefault()?
                .GetValue(entry.Entity);
            return value;
        }
    }
}