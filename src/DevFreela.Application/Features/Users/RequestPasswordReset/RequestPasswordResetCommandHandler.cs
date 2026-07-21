using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.RequestPasswordReset
{
    public class RequestPasswordResetCommandHandler(IUnitOfWork unitOfWork, IPasswordResetService resetService) : IRequestHandler<RequestPasswordResetCommand, Result>
    {
        public async Task<Result> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(request.Email, false, cancellationToken);

            if (user == null)
            {
                return Result.Failure(ValidationRules.UserNotFoundValidationMessage);
            }

            await resetService.SendPasswordResetCodeAsync(request.Email, cancellationToken);

            return Result.Success();
        }
    }
}
