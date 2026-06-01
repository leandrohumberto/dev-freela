using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public record CreateUserCommand(string FullName, string Email, DateOnly BirthDate, string Password, UserRole Role) : IRequest<Result<CreateUserResponse>>
    {
        public User ToEntity(string? passwordHash = null) => new(FullName, Email, BirthDate, passwordHash ?? Password, Role);
    }
}
