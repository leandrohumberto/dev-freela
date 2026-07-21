using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.ResetPassword
{
    public class ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordResetService resetService, IPasswordHasher hasher) : IRequestHandler<ResetPasswordCommand, Result>
    {
        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(request.Email, false, cancellationToken);

            if (user == null)
            {
                return Result.Failure(ValidationRules.UserNotFoundValidationMessage);
            }

            if (!resetService.ValidatePasswordResetCode(request.Email, request.Code))
            {
                return Result.Failure(ValidationRules.InvalidResetPasswordCodeValidationMessage);
            }

            var newPasswordHash = hasher.Hash(request.NewPassword);
            user.UpdatePasswordHash(newPasswordHash);

            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
