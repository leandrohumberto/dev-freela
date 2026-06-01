using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Users.ValidatePasswordResetCode
{
    public record ValidatePasswordResetCodeCommand(string Email, string Code) : IRequest<Result>;
}
