using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher hasher) : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
    {
        public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await unitOfWork.Users.ExistsAsync(request.Email, false, cancellationToken))
            {
                return Result.Failure<CreateUserResponse>(ValidationRules.UserAlreadyExistsValidationMessage);
            }

            var user = request.ToEntity(hasher.Hash(request.Password));

            await unitOfWork.Users.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success(CreateUserResponse.FromEntity(user));
        }
    }
}
