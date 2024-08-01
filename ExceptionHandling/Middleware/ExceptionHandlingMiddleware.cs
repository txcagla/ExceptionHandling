using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExceptionHandling.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Error handling can be done here
                await HandleExceptionAsync(context, ex);
            }

            // Catch 404 errors
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                await HandleNotFoundAsync(context);
            }
        }

        private Task HandleNotFoundAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            // Redirect to the 404 page
            context.Request.Path = "/Error/NotFound";
            return _next(context); // Further processing after redirection
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            // Redirect to the error page
            context.Request.Path = "/Error/Index";
            return _next(context);
        }
    }
}
