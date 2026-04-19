using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project?> GetByIdAsync(Guid projectId, bool deleted = false, CancellationToken cancellationToken = default);
        Task<List<Project>> SearchAsync(string? title = "", string? description = "", int page = 0, int size = 100, bool deleted = false, CancellationToken cancellationToken = default);
        void Update(Project project);
        Task AddCommentAsync(ProjectComment comment, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
