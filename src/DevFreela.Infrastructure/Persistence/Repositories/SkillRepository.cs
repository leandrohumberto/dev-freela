using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository(DevFreelaDbContext context) : ISkillRepository
    {
        public async Task AddAsync(Skill skill, CancellationToken cancellationToken = default)
        {
            await context.Skills.AddAsync(skill, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Skills.AnyAsync(s => s.Id == id && s.Deleted == deleted, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string description, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Skills.AnyAsync(s => s.Description == description && s.Deleted == deleted, cancellationToken);
        }

        public async Task<List<Skill>> GetAllAsync(bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Skills.Where(s => s.Deleted == deleted).ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
