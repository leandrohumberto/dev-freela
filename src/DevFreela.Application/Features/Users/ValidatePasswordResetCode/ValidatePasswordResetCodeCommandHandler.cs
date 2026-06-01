using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.ValidatePasswordResetCode
{
    public class ValidatePasswordResetCodeCommandHandler(IPasswordResetService resetService) : IRequestHandler<ValidatePasswordResetCodeCommand, Result>
    {
        public Task<Result> Handle(ValidatePasswordResetCodeCommand request, CancellationToken cancellationToken)
        {
            if (!resetService.ValidatePasswordResetCode(request.Email, request.Code))
            {
                return Task.FromResult(Result.Failure(ValidationRules.InvalidResetPasswordCodeValidationMessage));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
