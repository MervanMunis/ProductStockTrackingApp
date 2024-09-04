using Entities.DTOs.StockDTO;
using Entities.DTOs.ProductStockReportDTO;
using Services.Exceptions;

namespace Services.Contracts
{
    public interface IStockService
    {
        Task<ServiceResult<IEnumerable<StockResponse>>> GetAllStocksAsync(bool trackChanges);
        Task<ServiceResult<IEnumerable<StockResponse>>> GetAllActiveStocksAsync(bool isDeleted, bool trackChanges);
        Task<ServiceResult<StockResponse>> GetStockByIdAsync(Guid id, bool trackChanges);
        Task<ServiceResult<string>> CreateStockAsync(StockRequest stockRequest);
        Task<ServiceResult<string>> UpdateStockAsync(Guid id, StockRequest stockRequest, bool trackChanges);
        Task<ServiceResult<IEnumerable<ProductStockReportResponse>>> GetProductStockReportAsync(bool trackChanges);
        Task<ServiceResult<string>> DeleteStockAsync(Guid id, bool trackChanges);
    }
}
