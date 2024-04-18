using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Net;

namespace DeviceManagementWeb.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggingService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = ex switch
            {
                AccessViolationException => "Acces Violation Error.",
                _ => "Internal Server Error."
            };

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message,
            }.ToString());
        }
    }
}
