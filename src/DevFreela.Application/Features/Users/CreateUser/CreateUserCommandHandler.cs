using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public class CreateUserCommandhandler(DevFreelaDbContext context) : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(user.Id);
        }
    }
}
