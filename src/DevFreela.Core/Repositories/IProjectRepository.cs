using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project?> GetByIdAsync(Guid projectId, bool deleted = false, CancellationToken cancellationToken = default);
        Task<PaginationResult<Project>> SearchAsync(string? title = "", string? description = "", int page = 1, int pageSize = 10, bool deleted = false, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid projectId, CancellationToken cancellationToken = default);
        void Update(Project project);
        Task AddCommentAsync(ProjectComment comment, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
