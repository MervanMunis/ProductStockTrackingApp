using Entities.DTOs.StockDTO;
using Entities.DTOs.ProductStockReportDTO;

namespace Services.Contracts
{
    public interface IStockService
    {
        Task<IEnumerable<StockResponse>> GetAllStocksAsync(bool trackChanges);
        Task<StockResponse> GetStockByIdAsync(Guid id, bool trackChanges);
        Task<StockResponse> CreateStockAsync(StockRequest stockRequest);
        Task UpdateStockAsync(Guid id, StockRequest stockRequest, bool trackChanges);

        Task<IEnumerable<ProductStockReportResponse>> GetProductStockReportAsync(bool trackChanges);
        Task DeleteStockAsync(Guid id, bool trackChanges);
    }
}
