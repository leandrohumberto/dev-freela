using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task AddAsync(Skill skill, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, bool deleted = false, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string description, bool deleted = false, CancellationToken cancellationToken = default);
        Task<List<Skill>> GetAllAsync(bool deleted = false, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
