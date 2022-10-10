using System.Net;

namespace ECommerce.Shared.Exceptions
{
    public class ForBiddenException : HttpException
    {
        public ForBiddenException(string message) : base(HttpStatusCode.Forbidden, message)
        {
        }

        public ForBiddenException(string message, Exception innerException) : base(HttpStatusCode.Forbidden, message, innerException)
        {
        }
    }
}