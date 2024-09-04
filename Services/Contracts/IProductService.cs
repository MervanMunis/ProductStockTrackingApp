using Entities.DTOs.ProductDTO;
using Services.Exceptions;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<ServiceResult<IEnumerable<ProductResponse>>> GetAllProductsAsync(bool trackChanges);
        Task<ServiceResult<IEnumerable<ProductResponse>>> GetActiveProductsAsync(bool isDeleted, bool trackChanges);
        Task<ServiceResult<ProductResponse>> GetProductByIdAsync(Guid id, bool trackChanges);
        Task<ServiceResult<string>> CreateProductAsync(ProductRequest productRequest);
        Task<ServiceResult<string>> UpdateProductAsync(Guid id, ProductRequest productRequest, bool trackChanges);
        Task<ServiceResult<string>> DeleteProductAsync(Guid id, bool trackChanges);
    }
}
