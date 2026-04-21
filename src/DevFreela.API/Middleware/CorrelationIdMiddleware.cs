using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace DevFreela.API.Middleware
{
    public class CorrelationIdMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.TryGetValue("X-Correlation-Id", out StringValues correlationIdHeader);

            var correlationId = correlationIdHeader.FirstOrDefault() ?? Guid.NewGuid().ToString();

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next(context);
            }
        }
    }
}
