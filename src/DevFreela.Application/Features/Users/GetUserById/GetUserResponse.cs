using DevFreela.Core.Entities;

namespace DevFreela.Application.Features.Users.GetUserById
{
    public record GetUserResponse(string FullName, string Email, DateOnly BirthDate, List<string> Skills)
    {
        public static GetUserResponse FromEntity(User user) => new(
            user.FullName,
            user.Email,
            user.BirthDate,
            [.. user.Skills
                .Select(s => s.Skill?.Description)
                .Where(d => d != null)!]);
    }
}
