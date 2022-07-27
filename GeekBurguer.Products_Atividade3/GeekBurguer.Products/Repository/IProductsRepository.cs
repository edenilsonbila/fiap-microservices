using GeekBurguer.Products.Models;
using System;
using System.Collections.Generic;

namespace GeekBurguer.Products.Repository
{
    public interface IProductsRepository
    {
       public IEnumerable<Product> GetProductsByStoreName(string storeName);
       public bool Add(Product product);

       public List<Item> GetFullListOfItems();

       public void Delete(Product product);

       public void Save();

       public bool Update(Product product);

       Product GetProductById(Guid productId);
    }
}
