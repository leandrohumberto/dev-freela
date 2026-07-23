using DevFreela.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    internal sealed class Transaction(IDbContextTransaction efTransaction) : ITransaction
    {
        private readonly IDbContextTransaction _efTransaction = efTransaction;
        private bool _committed;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _efTransaction.CommitAsync(cancellationToken);
            _committed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (!_committed)
            {
                await _efTransaction.RollbackAsync();
            }

            await _efTransaction.DisposeAsync();
        }
    }
}
