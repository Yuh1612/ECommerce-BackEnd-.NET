using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Shared.Interfaces
{
    public interface IHttpResult : IResultBase
    {
        [JsonIgnore]
        HttpStatusCode StatusCode { get; set; }
    }

    public interface IHttpResult<T> : IHttpResult, IResultBase<T>
    {
    }
}