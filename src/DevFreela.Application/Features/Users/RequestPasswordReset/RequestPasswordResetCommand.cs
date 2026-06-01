using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.RequestPasswordReset
{
    public record RequestPasswordResetCommand(string Email) : IRequest<Result>;
}
