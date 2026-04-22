using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DevFreela.API.Handlers
{
    public class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment environment)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode;
            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.StatusCode = statusCode;

                    var errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    var validationResponse = JsonSerializer.Serialize(new { Errors = errors });
                    await httpContext.Response.WriteAsync(validationResponse, cancellationToken);
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.StatusCode = statusCode;

                    var response = JsonSerializer.Serialize(new ProblemDetails
                    {
                        Status = statusCode,
                        Title = "Internal Server Error.",
                        Detail = environment.IsDevelopment() ? exception.Message : null
                    });

                    await httpContext.Response.WriteAsync(response, cancellationToken);
                    break;
            }

            logger.LogError(
                exception,
                "An unhandled exception has occurred. Path: {Path}, Status: {StatusCode}",
                httpContext.Request.Path,
                statusCode);

            return true;
        }
    }
}
