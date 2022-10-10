using System.Data;

namespace Ecommerce.Utilities.EFCore.Attributes
{
    public class SqlParameterAttribute : Attribute
    {
        public string? ParameterName { get; set; }
        private SqlDbType? _parameterType;
        private string? _typeName;

        public SqlDbType ParameterType
        {
            get => _parameterType ?? SqlDbType.NVarChar;
            set => _parameterType = value;
        }

        public ParameterDirection ParameterDirection { get; set; } = ParameterDirection.Input;

        public string? TypeName
        {
            get => _typeName;
            set => _typeName = value;
        }

        public SqlParameterAttribute(string? name = null)
        {
            ParameterName = name;
        }

        public SqlDbType? GetParameterType() => _parameterType;

        public string? GetParameterTypeName() => _typeName;
    }
}