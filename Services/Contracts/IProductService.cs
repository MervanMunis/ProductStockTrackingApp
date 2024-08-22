using Entities.DTOs.ProductDTO;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(bool trackChanges);
        Task<ProductResponse> GetProductByIdAsync(Guid id, bool trackChanges);
        Task<ProductResponse> CreateProductAsync(ProductRequest productRequest);
        Task UpdateProductAsync(Guid id, ProductRequest productRequest, bool trackChanges);
        Task DeleteProductAsync(Guid id, bool trackChanges);
    }
}
