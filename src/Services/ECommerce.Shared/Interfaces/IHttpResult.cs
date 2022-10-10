using System.Net;
using System.Text.Json.Serialization;

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