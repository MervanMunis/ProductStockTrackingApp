using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneProduct(Product product) => Create(product);
        public void DeleteOneProduct(Product product) => Delete(product);
        public IQueryable<Product> GetAllProducts(bool trackChanges) => 
            FindAll(trackChanges);
        public Product GetOneProductById(Guid id, bool trackChanges) =>
            FindByCondition(p => p.UUID.Equals(id), trackChanges)
            .SingleOrDefault()!;
        public void UpdateOneProduct(Product product) => Update(product);
    }
}
