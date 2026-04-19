using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public record CreateUserCommand(string FullName, string Email, DateOnly BirthDate) : IRequest<Result<Guid>>
    {
        public User ToEntity() => new(FullName, Email, BirthDate);
    }
}
