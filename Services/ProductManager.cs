using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public Product CreateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _manager.Product.Create(product);
            _manager.Save();
            return product;
        }

        public void DeleteProduct(Guid id, bool trackChanges)
        {
            //Check entity
            var product = _manager.Product.GetOneProductById(id, trackChanges);

            if (product is null)
            {
                throw new Exception($"Book with id: {id} could not been found.");
            }
            _manager.Product.Delete(product);
            _manager.Save();
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Product GetProductById(Guid id, bool trackChanges)
        {
            return _manager.Product.GetOneProductById(id, trackChanges);
        }

        public void UpdateProduct(Guid id, Product product, bool trackChanges)
        {
            var entity = _manager.Product.GetOneProductById(id, trackChanges);

            if (entity is null)
            {
                throw new Exception($"Book with id: {id} could not been found.");
            }

            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            entity.Name = product.Name;
            
            _manager.Product.Update(entity);
            _manager.Save();
        }
    }
}
