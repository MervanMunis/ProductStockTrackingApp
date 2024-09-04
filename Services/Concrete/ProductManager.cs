using AutoMapper;
using Entities.DTOs.ProductDTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using Services.Exceptions;

namespace Services.Concrete
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

        public async Task<ServiceResult<string>> CreateProductAsync(ProductRequest productRequest)
        {
            if (productRequest.Name == null)
                return ServiceResult<string>.FailureResult($"Product name cannot be empty!");

            bool product = _repositoryManager.ProductRepository
                .Any(p => p.Name == productRequest!.Name);

            if (product)
                return ServiceResult<string>.FailureResult($"The product with name: {productRequest.Name} already exists!");

            var productEntity = _mapper.Map<Product>(productRequest);
            productEntity.CreationTime = DateTime.UtcNow;

            await _repositoryManager.ProductRepository.CreateAsync(productEntity);
            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("The product is created.");
        }

        public async Task<ServiceResult<IEnumerable<ProductResponse>>> GetAllProductsAsync(bool trackChanges)
        {
            var products = await _repositoryManager.ProductRepository.GetAllAsync(trackChanges);
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return ServiceResult<IEnumerable<ProductResponse>>.SuccessResult(productResponses);
        }

        public async Task<ServiceResult<IEnumerable<ProductResponse>>> GetActiveProductsAsync(bool isDeleted, bool trackChanges)
        {
            var products = await _repositoryManager.ProductRepository.GetByIsDeletedStatusAsync(isDeleted, trackChanges);
            var productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return ServiceResult<IEnumerable<ProductResponse>>.SuccessResult(productResponse);
        }

        public async Task<ServiceResult<ProductResponse>> GetProductByIdAsync(Guid id, bool trackChanges)
        {
            var product = await _repositoryManager.ProductRepository.GetByIdAsync(id, trackChanges);

            if (product == null || product!.IsDeleted == true)
                return ServiceResult<ProductResponse>.FailureResult("The product could not be found!");

            var productResponse = _mapper.Map<ProductResponse>(product);
            return ServiceResult<ProductResponse>.SuccessResult(productResponse);
        }

        public async Task<ServiceResult<string>> UpdateProductAsync(Guid id, ProductRequest productRequest, bool trackChanges)
        {
            if (productRequest.Name == null)
                return ServiceResult<string>.FailureResult("The product name cannot be empty!");

            bool product = _repositoryManager.ProductRepository
                .Any(p => p.Name == productRequest!.Name);

            if (product)
                return ServiceResult<string>.FailureResult($"The product with name: {productRequest.Name} already exists!");

            var productEntity = await _repositoryManager.ProductRepository.GetByIdAsync(id, trackChanges);

            if (productEntity == null || productEntity!.IsDeleted is true)
                return ServiceResult<string>.FailureResult($"Product with id: {id} could not be found!");

            _mapper.Map(productRequest, productEntity);

            _repositoryManager.ProductRepository.Update(productEntity);
            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("The product is updated.");
        }

        public async Task<ServiceResult<string>> DeleteProductAsync(Guid id, bool trackChanges)
        {
            var productEntity = await _repositoryManager.ProductRepository.GetByIdAsync(id, trackChanges);

            if (productEntity.IsDeleted == true)
                return ServiceResult<string>.FailureResult("The product is already deleted!");

            if (productEntity == null)
                return ServiceResult<string>.FailureResult($"Product with id: {id} could not be found.");
            
            productEntity.IsDeleted = true;
            productEntity.DeletionTime = DateTime.UtcNow;

            // Fetch all related stocks and mark them as deleted
            var relatedStocks = await _repositoryManager.StockRepository
                .GetByProductIdAsync(productEntity.UUID, trackChanges);

            foreach (var stock in relatedStocks)
            {
                stock.IsDeleted = true;
                _repositoryManager.StockRepository.Update(stock);
            }

            _repositoryManager.ProductRepository.Update(productEntity);
            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("The product is deleted.");
        }
    }
}
