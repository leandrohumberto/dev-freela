using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.GetUser
{
    public class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
    {
        public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.UserId, cancellationToken);

            if (!exists)
            {
                return Result.Failure<GetUserResponse>(ValidationRules.UserNotFoundValidationMessage);
            }

            var user = await repository.GetByIdAsync(request.UserId, false, cancellationToken);

            return Result.Success(GetUserResponse.FromEntity(user!));
        }
    }
}
