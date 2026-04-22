using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid userId, bool deleted = false, CancellationToken cancellationToken = default);
        Task AddSkillsAsync(List<UserSkill> skills, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
