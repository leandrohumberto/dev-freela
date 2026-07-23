using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork(
        DevFreelaDbContext context,
        IProjectRepository projects,
        ISkillRepository skills,
        IUserRepository users) : IUnitOfWork
    {
        private readonly DevFreelaDbContext _context = context;

        public IProjectRepository Projects { get; } = projects;

        public ISkillRepository Skills { get; } = skills;

        public IUserRepository Users { get; } = users;

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var efTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new Transaction(efTransaction);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            await _context.DisposeAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
