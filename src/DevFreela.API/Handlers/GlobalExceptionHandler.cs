using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
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
            string? title = null;
            string? detail = null;
            object? errors = null;

            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    title = "Validation error.";
                    detail = "One or more validation errors has occurred";

                    errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    break;

                case InvalidOperationException invalidOperationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    title = "Invalid Operation.";
                    detail = invalidOperationException.Message;

                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    title = "Internal Server Error.";
                    detail = environment.IsDevelopment() ? exception.Message : null;

                    break;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

            if (errors is not null)
            {
                problemDetails.Extensions["errors"] = errors;
            }

            var response = JsonSerializer.Serialize(problemDetails);

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(response, cancellationToken);

            logger.LogError(
                exception,
                "An unhandled exception has occurred. Path: {Path}, Status: {StatusCode}",
                httpContext.Request.Path,
                statusCode);

            return true;
        }
    }
}
