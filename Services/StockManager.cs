using AutoMapper;
using Entities.DTOs.ProductStockReportDTO;
using Entities.DTOs.StockDTO;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
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

        public async Task<StockResponse> CreateStockAsync(StockRequest stockRequest)
        {
            if (stockRequest == null)
            {
                throw new ArgumentNullException(nameof(stockRequest));
            }

            var stockEntity = _mapper.Map<Stock>(stockRequest);
            stockEntity.CreationTime = DateTime.UtcNow;

            await _repositoryManager.Stock.CreateAsync(stockEntity);
            await _repositoryManager.SaveAsync();

            var stockResponse = _mapper.Map<StockResponse>(stockEntity);
            return stockResponse;
        }

        public async Task<IEnumerable<StockResponse>> GetAllStocksAsync(bool trackChanges)
        {
            var stocks = await _repositoryManager.Stock.GetAllAsync(trackChanges);
            var stockResponses = _mapper.Map<IEnumerable<StockResponse>>(stocks);
            return stockResponses;
        }

        public async Task<IEnumerable<StockResponse>> GetStocksByIsDeletedStatusAsync(bool isDeleted, bool trackChanges)
        {
            var stocks = await _repositoryManager.Stock.GetByIsDeletedStatusAsync(isDeleted, trackChanges);
            return _mapper.Map<IEnumerable<StockResponse>>(stocks);
        }

        public async Task<IEnumerable<StockResponse>> GetAllStocksWithDeletedStatusAsync(bool trackChanges)
        {
            var stocks = await _repositoryManager.Stock.GetAllAsync(trackChanges);
            return _mapper.Map<IEnumerable<StockResponse>>(stocks);
        }

        public async Task<StockResponse> GetStockByIdAsync(Guid id, bool trackChanges)
        {
            var stock = await _repositoryManager.Stock.GetByIdAsync(id, trackChanges);
            if (stock == null)
            {
                throw new Exception($"Stock with id: {id} could not be found.");
            }

            var stockResponse = _mapper.Map<StockResponse>(stock);
            return stockResponse;
        }

        public async Task UpdateStockAsync(Guid id, StockRequest stockRequest, bool trackChanges)
        {
            var stockEntity = await _repositoryManager.Stock.GetByIdAsync(id, trackChanges);

            if (stockEntity == null)
            {
                throw new Exception($"Stock with id: {id} could not be found.");
            }

            if (stockRequest == null)
            {
                throw new ArgumentNullException(nameof(stockRequest));
            }

            _mapper.Map(stockRequest, stockEntity);

            _repositoryManager.Stock.Update(stockEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<ProductStockReportResponse>> GetProductStockReportAsync(bool trackChanges)
        {
            var stocks = await _repositoryManager.Stock.GetAllWithProductAsync(trackChanges); // Use the new method

            var report = stocks.Select(stock => new ProductStockReportResponse
            {
                ProductId = stock.ProductId,
                ProductName = stock.Product!.Name,
                StockId = stock.UUID,
                StockQuantity = stock.Quantity,
                StockCreationTime = stock.CreationTime
            });

            return report;
        }

        public async Task DeleteStockAsync(Guid id, bool trackChanges)
        {
            var stockEntity = await _repositoryManager.Stock.GetByIdAsync(id, trackChanges);

            if (stockEntity == null)
            {
                throw new Exception($"Stock with id: {id} could not be found.");
            }

            stockEntity.IsDeleted = true;

            _repositoryManager.Stock.Update(stockEntity);
            await _repositoryManager.SaveAsync();
        }
    }
}
