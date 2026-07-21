using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.GetUser
{
    public class GetUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
    {
        public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Users.ExistsAsync(request.UserId, cancellationToken);

            if (!exists)
            {
                return Result.Failure<GetUserResponse>(ValidationRules.UserNotFoundValidationMessage);
            }

            var user = await unitOfWork.Users.GetByIdAsync(request.UserId, false, cancellationToken);

            return Result.Success(GetUserResponse.FromEntity(user!));
        }
    }
}
