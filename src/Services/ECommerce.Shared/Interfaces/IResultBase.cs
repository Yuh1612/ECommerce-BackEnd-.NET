using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Interfaces
{
    public interface IResultBase
    {
        bool IsSuccess { get; set; }
        string? Message { get; set; }
    }

    public interface IResultBase<T> : IResultBase
    {
        T? Data { get; set; }
    }
}