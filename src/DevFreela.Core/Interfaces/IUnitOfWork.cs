using DevFreela.Core.Repositories;

namespace DevFreela.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ISkillRepository Skills { get; }
        IUserRepository Users { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
