using AutoMapper;
using Entities.DTOs.ProductDTO;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest productRequest)
        {
            if (productRequest is null)
            {
                throw new ArgumentNullException(nameof(productRequest));
            }

            var productEntity = _mapper.Map<Product>(productRequest);
            productEntity.CreationTime = DateTime.UtcNow;

            await _repositoryManager.Product.CreateAsync(productEntity);
            await _repositoryManager.SaveAsync();

            var productResponse = _mapper.Map<ProductResponse>(productEntity);
            return productResponse;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(bool trackChanges)
        {
            var products = await _repositoryManager.Product.GetAllAsync(trackChanges);
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return productResponses;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsByIsDeletedStatusAsync(bool isDeleted, bool trackChanges)
        {
            var products = await _repositoryManager.Product.GetByIsDeletedStatusAsync(isDeleted, trackChanges);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsWithDeletedStatusAsync(bool trackChanges)
        {
            var products = await _repositoryManager.Product.GetAllAsync(trackChanges);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }
        public async Task<ProductResponse> GetProductByIdAsync(Guid id, bool trackChanges)
        {
            var product = await _repositoryManager.Product.GetByIdAsync(id, trackChanges);
            if (product == null)
            {
                throw new Exception($"Product with id: {id} could not be found.");
            }

            var productResponse = _mapper.Map<ProductResponse>(product);
            return productResponse;
        }

        public async Task UpdateProductAsync(Guid id, ProductRequest productRequest, bool trackChanges)
        {
            var productEntity = await _repositoryManager.Product.GetByIdAsync(id, trackChanges);

            if (productEntity == null)
            {
                throw new Exception($"Product with id: {id} could not be found.");
            }

            if (productRequest == null)
            {
                throw new ArgumentNullException(nameof(productRequest));
            }
            
            if (productEntity.IsDeleted is true)
            {
                throw new Exception($"Product with id: {id} could not be found.");
            }

            _mapper.Map(productRequest, productEntity);

            _repositoryManager.Product.Update(productEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id, bool trackChanges)
        {
            var productEntity = await _repositoryManager.Product.GetByIdAsync(id, trackChanges);

            if (productEntity == null)
            {
                throw new Exception($"Product with id: {id} could not be found.");
            }

            productEntity.IsDeleted = true;
            productEntity.DeletionTime = DateTime.UtcNow;

            // Fetch all related stocks and mark them as deleted
            var relatedStocks = await _repositoryManager.Stock.GetByProductIdAsync(productEntity.UUID, trackChanges);
            foreach (var stock in relatedStocks)
            {
                stock.IsDeleted = true;
                _repositoryManager.Stock.Update(stock);
            }

            _repositoryManager.Product.Update(productEntity);
            await _repositoryManager.SaveAsync();
        }
    }
}
