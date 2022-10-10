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
    }
}