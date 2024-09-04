namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        IStockRepository StockRepository { get; }
        Task SaveAsync();
    }
}
