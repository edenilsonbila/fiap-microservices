using GeekBurguer.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurguer.Products.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsDbContext _ctx;
   
        public ProductsRepository(ProductsDbContext context)
        {
            _ctx = context;

        }

        public bool Add(Product product)
        {
            product.ProductId = Guid.NewGuid();
            _ctx.Products.Add(product);
            return true;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public IEnumerable<Product> GetProductsByStoreName(string storeName)
        {
            var products = _ctx.Products.Where(p => p.Store.Name.Equals(storeName,
                StringComparison.InvariantCultureIgnoreCase)).Include(p => p.Items).AsEnumerable();

            return products;
        }

        public List<Item> GetFullListOfItems()
        {
            return _ctx.Items.ToList();
        }

        public void Delete(Product product)
        {
            _ctx.Products.Remove(product);
        }

        public bool Update(Product product)
        {
            _ctx.Entry(product).State = EntityState.Modified;
            return true;
        }

        public Product GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
