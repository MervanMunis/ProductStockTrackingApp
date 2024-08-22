namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IStockService StockService { get; }
    }
}
