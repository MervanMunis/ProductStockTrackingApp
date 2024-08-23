using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        private readonly Lazy<IStockService> _stockService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager, mapper));
            _stockService = new Lazy<IStockService>(() => new StockManager(repositoryManager, mapper));
        }
        public IProductService ProductService => _productService.Value;

        public IStockService StockService => _stockService.Value;
    }
}
