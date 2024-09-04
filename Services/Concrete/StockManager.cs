using AutoMapper;
using Entities.DTOs.ProductDTO;
using Entities.DTOs.ProductStockReportDTO;
using Entities.DTOs.StockDTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using Services.Exceptions;

namespace Services.Concrete
{
    public class StockManager : IStockService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public StockManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ServiceResult<string>> CreateStockAsync(StockRequest stockRequest)
        {
            if (stockRequest == null)
                return ServiceResult<string>.FailureResult("Stock request cannot be empty!");

            var product = await _repositoryManager.ProductRepository.GetByIdAsync(stockRequest.ProductId, false);

            if (product == null || product.IsDeleted == true)
                return ServiceResult<string>.FailureResult("The product is not found!");

            var stock = await _repositoryManager.StockRepository
                .FindByCondition(s => s.ProductId == stockRequest.ProductId, false)
                .FirstOrDefaultAsync();

            if (stock != null && stock!.Quantity >= 0)
                return ServiceResult<string>.FailureResult($"The stock already exists for the id: {stockRequest.ProductId} number!");

            var stockEntity = _mapper.Map<Stock>(stockRequest);

            stockEntity.CreationTime = DateTime.UtcNow;

            await _repositoryManager.StockRepository.CreateAsync(stockEntity);

            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("The stock is created.");
        }

        public async Task<ServiceResult<IEnumerable<StockResponse>>> GetAllStocksAsync(bool trackChanges)
        {
            var stocks = await _repositoryManager.StockRepository.GetAllAsync(trackChanges);
            var stockResponses = _mapper.Map<IEnumerable<StockResponse>>(stocks);

            return ServiceResult<IEnumerable<StockResponse>>.SuccessResult(stockResponses);
        }

        public async Task<ServiceResult<IEnumerable<StockResponse>>> GetAllActiveStocksAsync(bool isDeleted, bool trackChanges)
        {
            var stocks = await _repositoryManager.StockRepository.GetByIsDeletedStatusAsync(isDeleted, trackChanges);
            var stockResponses = _mapper.Map<IEnumerable<StockResponse>>(stocks);

            return ServiceResult<IEnumerable<StockResponse>>.SuccessResult(stockResponses); 
        }

        public async Task<ServiceResult<StockResponse>> GetStockByIdAsync(Guid id, bool trackChanges)
        {
            var stock = await _repositoryManager.StockRepository.GetByIdAsync(id, trackChanges);

            if (stock == null || stock!.IsDeleted == true)
                return ServiceResult<StockResponse>.FailureResult($"The stock with id: {id} could not be found!");

            var stockResponse = _mapper.Map<StockResponse>(stock);

            return ServiceResult<StockResponse>.SuccessResult(stockResponse);
        }

        public async Task<ServiceResult<string>> UpdateStockAsync(Guid id, StockRequest stockRequest, bool trackChanges)
        {
            if (stockRequest == null)
                return ServiceResult<string>.FailureResult("Stock request can not be empty!");

            var product = await _repositoryManager.ProductRepository.GetByIdAsync(stockRequest.ProductId, false);

            if (product == null || product.IsDeleted == true)
                return ServiceResult<string>.FailureResult("The product is not found!");

            var stockEntity = await _repositoryManager.StockRepository.GetByIdAsync(id, trackChanges);

            if (stockEntity == null || stockEntity!.IsDeleted is true)
                return ServiceResult<string>.FailureResult($"Stock with id: {id} could not be found!");
            
            _mapper.Map(stockRequest, stockEntity);

            _repositoryManager.StockRepository.Update(stockEntity);
            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("Stock is updated.");
        }

        public async Task<ServiceResult<IEnumerable<ProductStockReportResponse>>> GetProductStockReportAsync(bool trackChanges)
        {
            var stocks = await _repositoryManager.StockRepository.GetStocksWithProductAsync(trackChanges);

            var report = stocks.Select(stock => new ProductStockReportResponse
            {
                ProductId = stock.ProductId,
                ProductName = stock.Product!.Name,
                StockId = stock.UUID,
                StockQuantity = stock.Quantity,
                StockCreationTime = stock.CreationTime
            });

            if (report == null)
                return ServiceResult<IEnumerable<ProductStockReportResponse>>.FailureResult("The report could not be generated!");

            return ServiceResult<IEnumerable<ProductStockReportResponse>>.SuccessResult(report);
        }

        public async Task<ServiceResult<string>> DeleteStockAsync(Guid id, bool trackChanges)
        {
            var stockEntity = await _repositoryManager.StockRepository.GetByIdAsync(id, trackChanges);

            if (stockEntity == null)
                return ServiceResult<string>.FailureResult($"Stock with id: {id} could not be found!");

            if (stockEntity.IsDeleted == true)
                return ServiceResult<string>.FailureResult("The stock is already deleted!");            
            
            stockEntity.IsDeleted = true;

            _repositoryManager.StockRepository.Update(stockEntity);
            await _repositoryManager.SaveAsync();

            return ServiceResult<string>.SuccessResult("The stock is deleted.");
        }
    }
}
