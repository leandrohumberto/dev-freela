using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevFreela.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (errors.Count != 0)
                {
                    logger.LogWarning("Validation failed for request {RequestName}. Errors: {ValidationErrors}", typeof(TRequest).Name, errors);
                    throw new ValidationException(errors);
                }
            }

            return await next(cancellationToken);
        }
    }
}
