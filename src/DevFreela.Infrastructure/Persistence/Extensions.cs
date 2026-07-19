using DevFreela.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static async Task<PaginationResult<T>> GetPaged<T>
            (this IQueryable<T> query,
            int page,
            int pageSize) where T : class
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(page);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

            var itemsCount = await query.CountAsync();

            var pageCount = (double)itemsCount / pageSize;
            var totalPages = (int)Math.Ceiling(pageCount);
            
            var itemsToSkip = (page  - 1) * pageSize;
            List<T> data = await query.Skip(itemsToSkip).Take(pageSize).ToListAsync();

            return new PaginationResult<T>(page, totalPages, pageSize, itemsCount, data);
        }
    }
}
