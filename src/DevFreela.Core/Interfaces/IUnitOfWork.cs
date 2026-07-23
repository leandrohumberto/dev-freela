using DevFreela.Core.Repositories;

namespace DevFreela.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IProjectRepository Projects { get; }
        ISkillRepository Skills { get; }
        IUserRepository Users { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a transaction. Returns an <see cref="ITransaction"/> that must be committed explicitly;
        /// if disposed without commit, the transaction is rolled back automatically.
        /// </summary>
        /// <example>
        /// await using var tx = await _unitOfWork.BeginTransactionAsync(ct);
        /// // ... work ...
        /// await _unitOfWork.CompleteAsync(ct);
        /// await tx.CommitAsync(ct);
        /// </example>
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
