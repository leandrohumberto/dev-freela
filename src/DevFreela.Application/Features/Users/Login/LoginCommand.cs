using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;
}
