using Entities.Models;

namespace Repositories.Contracts
{
    public interface IStockRepository : IRepositoryBase<Stock>
    {
        Task<IEnumerable<Stock>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<Stock>> GetByIsDeletedStatusAsync(bool isDeleted, bool trackChanges);
        Task<Stock> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Stock>> GetAllWithProductAsync(bool trackChanges);
        Task<IEnumerable<Stock>> GetByProductIdAsync(Guid productId, bool trackChanges);
    }
}
