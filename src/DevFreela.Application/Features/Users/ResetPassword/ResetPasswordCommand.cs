using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.ResetPassword
{
    public record ResetPasswordCommand(string Email, string Code, string NewPassword) : IRequest<Result>;
}
