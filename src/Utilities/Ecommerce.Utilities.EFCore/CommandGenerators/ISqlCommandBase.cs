using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Utilities.EFCore.CommandGenerators
{
    public interface ISqlCommandBase
    {
        string? Sql { get; set; }
        CommandType CommandType { get; set; }
        SqlParameter[] Parameters { get; set; }

        T? OutputValue<T>(string outputProperty);

        object? OutputValue(string outputProperty);

        void LoadOutputParameters();
    }
}