using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Stock>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(s => s.CreationTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetByIsDeletedStatusAsync(bool isDeleted, bool trackChanges)
        {
            return await FindByCondition(s => s.IsDeleted == isDeleted, trackChanges).ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(s => s.UUID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksWithProductAsync(bool trackChanges) // New method
        {
            return await FindAll(trackChanges)
                .Include(s => s.Product) // Include the related Product entity
                .OrderBy(s => s.CreationTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetByProductIdAsync(Guid productId, bool trackChanges)
        {
            return await FindByCondition(s => s.ProductId.Equals(productId), trackChanges).ToListAsync();
        }
    }
}
