using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByIsDeletedStatusAsync(bool isDeleted, bool trackChanges)
        {
            return await FindByCondition(p => p.IsDeleted == isDeleted, trackChanges).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(p => p.UUID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
