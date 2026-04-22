using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.GetUser
{
    public record GetUserQuery(Guid UserId) : IRequest<Result<GetUserResponse>>;
}
