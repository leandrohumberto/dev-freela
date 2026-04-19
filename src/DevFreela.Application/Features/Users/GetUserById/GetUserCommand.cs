using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.GetUserById
{
    public record GetUserCommand(Guid UserId) : IRequest<Result<GetUserResponse>>;
}
