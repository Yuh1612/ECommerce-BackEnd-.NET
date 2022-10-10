using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Exceptions
{
    public class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException)
        {
        }
    }
}