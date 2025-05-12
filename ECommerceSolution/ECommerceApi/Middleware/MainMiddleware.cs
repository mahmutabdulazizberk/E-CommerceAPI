using System.Text.Json;
using Entity.Extension;
using Entity.Result;

namespace ECommerceApi.Middleware
{
    public class MainMiddleware
    {
        private readonly RequestDelegate _next;
        
        public MainMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                await HandleApiExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        
        private async Task HandleApiExceptionAsync(HttpContext context, ApiException exception)
        {
            await WriteResponseAsync(
                context,
                exception.StatusCode,
                exception.Message,
                exception.ErrorCode,
                exception.CustomData);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await WriteResponseAsync(
                context,
                500,
                "Internal Server Error",
                "INTERNAL_ERROR");
        }

        private async Task WriteResponseAsync(HttpContext context, int statusCode, string message, string errorCode, object? customData = null)
        {
            var response = Result<object>.ErrorResult(message, errorCode, customData);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
