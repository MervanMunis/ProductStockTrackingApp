using Entities.Models;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync(bool trackChanges);
        Task<Product> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Product>> GetByIsDeletedStatusAsync(bool isDeleted, bool trackChanges);
    }
}
