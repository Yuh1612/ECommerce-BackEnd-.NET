using System.Data;

namespace Ecommerce.Utilities.EFCore.Attributes
{
    public class SqlCommandAttribute : Attribute
    {
        public string Sql { get; set; }
        public CommandType CommandType { get; set; }

        public SqlCommandAttribute(string sql, CommandType commandType = CommandType.StoredProcedure)
        {
            Sql = sql;
            CommandType = commandType;
        }
    }
}