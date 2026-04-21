using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using InventoryService.Domain.Exceptions;
using InventoryService.Application.Exceptions;

namespace InventoryService.API
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

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case NotFoundException ex:
                    await WriteResponseAsync(context, StatusCodes.Status404NotFound, ex.Message);
                    break;
                case InsufficientStockException ex:
                    await WriteResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);
                    break;
                case ConcurrencyException ex:
                    await WriteResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);
                    break;
                default:
                    await WriteResponseAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
                    break;

            }
        }

        private async Task WriteResponseAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new {error = message};
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
