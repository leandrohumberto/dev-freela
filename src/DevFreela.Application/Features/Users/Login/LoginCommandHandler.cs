using DevFreela.Application.Common;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.Login
{
    public class LoginCommandHandler(
        IUserRepository repository,
        IPasswordHasher hasher,
        IAuthenticationService authentication)
        : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByEmailAsync(request.Email, false, cancellationToken);

            if (user == null || !hasher.Verify(user.PasswordHash, request.Password))
            {
                return Result.Failure<LoginResponse>("Invalid email or password.");
            }

            var token = authentication.GenerateJwtToken(user);

            return Result.Success(new LoginResponse(token));
        }
    }
}
