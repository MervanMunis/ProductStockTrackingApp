using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        Product GetProductById(Guid id, bool trackChanges);
        Product CreateProduct(Product product);
        void DeleteProduct(Guid id, bool trackChanges);
        void UpdateProduct(Guid id, Product product, bool trackChanges);

    }
}
