using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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