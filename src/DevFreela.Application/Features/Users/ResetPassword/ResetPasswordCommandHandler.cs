using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.ResetPassword
{
    public class ResetPasswordCommandHandler(IUserRepository repository, IPasswordResetService resetService, IPasswordHasher hasher) : IRequestHandler<ResetPasswordCommand, Result>
    {
        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByEmailAsync(request.Email, false, cancellationToken);

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

            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
