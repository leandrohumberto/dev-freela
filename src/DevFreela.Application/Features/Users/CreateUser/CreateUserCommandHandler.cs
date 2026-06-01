using DevFreela.Application.Common;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository, IPasswordHasher hasher) : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
    {
        public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity(hasher.Hash(request.Password));

            await repository.AddAsync(user, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(CreateUserResponse.FromEntity(user));
        }
    }
}
