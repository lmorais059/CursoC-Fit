using System.Net;
using System.Text.Json;
using Catalog.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppError err)
            {
                await HandleExceptionAsync(context, err.StatusCode, err.Message);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, HttpStatusCode status, string message)
        {
            ProblemDetails problem = new()
            {
                Status = (int)status,
                Detail = message
            };


            string? result = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(result);
        }
    }
}