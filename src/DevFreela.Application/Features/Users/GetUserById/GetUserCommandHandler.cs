using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Users.GetUserById
{
    public class GetUserCommandHandler(DevFreelaDbContext context) : IRequestHandler<GetUserCommand, Result<GetUserResponse>>
    {
        public async Task<Result<GetUserResponse>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                 .Include(u => u.Skills)
                    .ThenInclude(s => s.Skill)
                 .SingleOrDefaultAsync(
                    p => p.Id == request.UserId && !p.Deleted,
                    cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetUserResponse>("User not found.");
            }

            return Result.Success(GetUserResponse.FromEntity(user));
        }
    }
}
