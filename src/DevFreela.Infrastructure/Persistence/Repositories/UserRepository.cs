using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository(DevFreelaDbContext context) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await context.Users.AddAsync(user, cancellationToken);
        }

        public async Task AddSkillsAsync(List<UserSkill> skills, CancellationToken cancellationToken = default)
        {
            await context.AddRangeAsync(skills, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await context.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string email, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Users.AnyAsync(u => u.Email == email && u.Deleted == deleted, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid userId, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Users
                 .Include(u => u.Skills)
                    .ThenInclude(s => s.Skill)
                 .SingleOrDefaultAsync(
                    p => p.Id == userId && p.Deleted == deleted, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Users
                 .Include(u => u.Skills)
                    .ThenInclude(s => s.Skill)
                 .SingleOrDefaultAsync(
                    p => p.Email == email && p.Deleted == deleted, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
