using ECommerce.Shared.Interfaces;
using ECommerce.Shared.ViewModels.Base;
using System.Net;
using System.Text.Json.Serialization;

namespace ECommerce.Shared.ViewModels
{
    public class HttpResult : ResultBase, IHttpResult
    {
        public HttpResult()
        {
        }

        public HttpResult(bool isSuccess, string? message, HttpStatusCode statusCode) : base(isSuccess, message)
        {
            StatusCode = statusCode;
        }

        public HttpResult(IHttpResult result) : base(result.IsSuccess, result.Message)
        {
            this.StatusCode = result.StatusCode;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        #region Success results

        public static HttpResult OK(string? message = default, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResult(true, message, statusCode);
        }

        public static HttpResult<T> OK<T>(T data, string? message = default, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResult<T>(true, message, statusCode, data);
        }

        #endregion Success results

        #region Error results

        /// <summary>
        /// Common Error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Error status code</param>

        public static HttpResult Error(string? message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new HttpResult(false, message, statusCode);
        }

        public static HttpResult<T> Error<T>(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, T? data = default)
        {
            return new HttpResult<T>(false, message, statusCode, data);
        }

        /// <summary>
        /// Bad request
        /// </summary>
        /// <param name="message">Error message</param>
        public static HttpResult BadRequest(string message)
        {
            return Error(message, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Bad request
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="message">Error message</param>
        /// <param name="data">Error data</param>
        public static HttpResult<T> BadRequest<T>(string message, T? data = default)
        {
            return Error<T>(message, HttpStatusCode.BadRequest, data);
        }

        /// <summary>
        /// Unauthorized
        /// </summary>
        /// <param name="message">Error message</param>
        public static HttpResult Unauthorized(string message)
        {
            return Error(message, HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Forbidden
        /// </summary>
        /// <param name="message">Error message</param>
        public static HttpResult Forbidden(string message)
        {
            return Error(message, HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// Forbidden
        /// </summary>
        /// <param name="message">Error message</param>
        public static HttpResult<T> Forbidden<T>(string message)
        {
            return Error<T>(message, HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// Unauthorized
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="message">Error message</param>
        /// <param name="data">Error data</param>
        public static HttpResult<T> Unauthorized<T>(string message, T? data = default)
        {
            return Error<T>(message, HttpStatusCode.Unauthorized, data);
        }

        /// <summary>
        /// The error of Internal Server
        /// </summary>
        /// <param name="message">Error message</param>

        public static HttpResult InternalServerError(string? message)
        {
            return Error(message, HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// The error of Internal Server
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="message">Error message</param>
        /// <param name="data">Error data</param>
        public static HttpResult<T> InternalServerError<T>(string message, T? data = default)
        {
            return Error<T>(message, HttpStatusCode.InternalServerError, data);
        }

        #endregion Error results
    }

    public class HttpResult<T> : HttpResult, IResultBase<T>
    {
        public HttpResult()
        {
        }

        public HttpResult(bool isSuccess, string? message, HttpStatusCode statusCode, T? data)
            : base(isSuccess, message, statusCode)
        {
            Data = data;
        }

        public HttpResult(IHttpResult<T> result) : base(result)
        {
            Data = result.Data;
        }

        public T? Data { get; set; }
    }
}