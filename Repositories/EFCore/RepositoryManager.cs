using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IStockRepository> _stockRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _stockRepository = new Lazy<IStockRepository>(() => new StockRepository(_context));
        }

        public IProductRepository ProductRepository => _productRepository.Value;
        public IStockRepository StockRepository => _stockRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
