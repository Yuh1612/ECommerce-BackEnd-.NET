using System.Net;

namespace ECommerce.Shared.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException)
        {
        }
    }
}