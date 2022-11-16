using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using ECommerce.Shared.Exceptions;
using ECommerce.Shared.Extensions;
using ECommerce.Shared.ViewModels;
using ECommerce.Shared.Serializers;

namespace ELDesk.Shared.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                return;

            bool isThrowMsg;
            // Is http exception
            if (typeof(HttpException).IsAssignableFrom(ex.GetType()))
            {
                HttpException httpEx = (HttpException)ex;
                context.Response.StatusCode = (int)httpEx.StatusCode;

                if (httpEx.IsClientError()) _logger.LogWarning(ex.Message);
                isThrowMsg = httpEx.IsClientError();
            }
            // If exception is Domain exception, this is a badrequest response
            else if (typeof(DomainException).IsAssignableFrom(ex.GetType()))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                isThrowMsg = true;
                _logger.LogWarning(ex.Message);
            }
            // the Internal Server Error
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                isThrowMsg = false;
                _logger.LogError(ex, ex.Message);
            }

            // Prepare error message model to return to client
            var message = isThrowMsg ? ex.GetMessage() : "InternalServerError";
            var error = new ErrorViewModel(message, ex.StackTrace);

            // Return as json
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializerHelper.Serialize(error));
        }
    }
}