using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await repository.AddAsync(user, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(user.Id);
        }
    }
}
