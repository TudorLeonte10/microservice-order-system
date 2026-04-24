using PaymentService.Application.Exceptions;
using PaymentService.Domain.Exceptions;

namespace PaymentService.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex) 
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case NotFoundException notFoundEx:
                    await WriteErrorReponse(context, StatusCodes.Status404NotFound, notFoundEx.Message);
                    break;
                case InvalidPaymentStatusException invalidStatusEx:
                    await WriteErrorReponse(context, StatusCodes.Status400BadRequest, invalidStatusEx.Message);
                    break;
                default:
                    await WriteErrorReponse(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
                    break;
            }
        }

        private async Task WriteErrorReponse(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var errorResponse = new { error = message };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
