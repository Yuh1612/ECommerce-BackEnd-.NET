using System.Net;

namespace ECommerce.Shared.Exceptions
{
    public class HttpException : DomainException
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public bool IsClientError() => (int)StatusCode >= 400 && (int)StatusCode <= 499;

        public bool IsServerError() => (int)StatusCode >= 500 && (int)StatusCode <= 599;
    }
}