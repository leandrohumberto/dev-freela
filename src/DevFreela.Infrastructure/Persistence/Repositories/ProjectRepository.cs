using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository(DevFreelaDbContext context) : IProjectRepository
    {
        public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            await context.Projects.AddAsync(project, cancellationToken);
        }

        public async Task AddCommentAsync(ProjectComment comment, CancellationToken cancellationToken = default)
        {
            await context.ProjectComments.AddAsync(comment, cancellationToken);
        }

        public async Task<Project?> GetByIdAsync(Guid projectId, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Projects
                 .Include(p => p.Client)
                 .Include(p => p.Freelancer)
                 .Include(p => p.Comments)
                 .SingleOrDefaultAsync(p => p.Id == projectId && p.Deleted == deleted, cancellationToken);
        }

        public async Task<List<Project>> SearchAsync(string? title = "", string? description = "", int page = 0, int size = 100, bool deleted = false, CancellationToken cancellationToken = default)
        {
            return await context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => p.Deleted == deleted
                    && (string.IsNullOrWhiteSpace(title) || (title != null && p.Title.Contains(title)))
                    && (string.IsNullOrWhiteSpace(description) || (description != null && p.Description.Contains(description)))
                )
                .Skip(page * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public void Update(Project project)
        {
            context.Projects.Update(project);
        }

        public async Task<bool> ExistsAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            return await context.Projects.AnyAsync(p => p.Id == projectId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
