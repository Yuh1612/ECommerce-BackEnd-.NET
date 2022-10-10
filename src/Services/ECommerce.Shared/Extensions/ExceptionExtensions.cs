using System;
using System.Data.SqlClient;

namespace ECommerce.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string? GetMessage(this Exception ex)
        {
            while (ex?.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex?.Message;
        }

        public static bool IsForeignKeyConflicted(this Exception ex)
        {
            return ex is Microsoft.EntityFrameworkCore.DbUpdateException
                && ex.InnerException != null
                && ex.InnerException is SqlException
                && (ex.Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint")
                || ex.InnerException.Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"));
        }

        public static bool IsParentKey(this Exception ex)
        {
            return ex is Microsoft.EntityFrameworkCore.DbUpdateException
                && ex.InnerException != null
                && ex.InnerException is SqlException
                && (ex.Message.StartsWith("The DELETE statement conflicted with the SAME TABLE REFERENCE constraint")
                || ex.InnerException.Message.StartsWith("The DELETE statement conflicted with the SAME TABLE REFERENCE constraint"));
        }
    }
}