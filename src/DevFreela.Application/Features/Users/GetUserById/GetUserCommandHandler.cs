using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.GetUserById
{
    public class GetUserCommandHandler(IUserRepository repository) : IRequestHandler<GetUserCommand, Result<GetUserResponse>>
    {
        public async Task<Result<GetUserResponse>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.UserId, false,
                    cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetUserResponse>("User not found.");
            }

            return Result.Success(GetUserResponse.FromEntity(user));
        }
    }
}
