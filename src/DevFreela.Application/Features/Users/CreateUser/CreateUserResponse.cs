using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public record CreateUserResponse(Guid Id, string FullName, string Email, DateOnly BirthDate, UserRole Role)
    {
        public static CreateUserResponse FromEntity(User user) => new(
            user.Id,
            user.FullName,
            user.Email,
            user.BirthDate,
            user.Role);
    }
}
