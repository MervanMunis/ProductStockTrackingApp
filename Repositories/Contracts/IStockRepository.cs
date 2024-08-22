using Entities.Models;

namespace Repositories.Contracts
{
    public interface IStockRepository : IRepositoryBase<Stock>
    {
        Task<IEnumerable<Stock>> GetAllAsync(bool trackChanges);
        Task<Stock> GetByIdAsync(Guid id, bool trackChanges);

        Task<IEnumerable<Stock>> GetAllWithProductAsync(bool trackChanges);
    }
}
