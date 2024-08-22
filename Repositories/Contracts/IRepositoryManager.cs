namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        IStockRepository Stock { get; }
        Task SaveAsync();
    }
}
