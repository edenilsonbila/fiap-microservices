using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Model;
using GeekBurguer.Ingredientes.Repository.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Repository
{
    public class ProductRepository : IProductRepository
    {
        private const string URL_API_PRODUCTS = "https://geekburger-products.azurewebsites.net/api/products";//"http://localhost:54972/api/products";
        private readonly ProductsDbContext _context;
        private readonly List<ProductIngredients> _db;

        
        public ProductRepository(ProductsDbContext context)
        {
            _context = context;
            _db = new List<ProductIngredients>();
        }
        
        
        public bool Add(ProductIngredients product)
        {
            _context.ProductIgredients.Add(product);
            _context.SaveChanges();
            return true;
        }

        public bool AddRange(IEnumerable<ProductIngredients> products)
        {
            _db.AddRange(products);
            /*
            _context.ProductIgredients.AddRange(products);
            _context.SaveChanges();*/
            return true;
        }

        public List<ProductIngredients> GetAll()
        {
            return _db;
        }

        public async Task<List<ProductToGet>> GetByStoreName(string storeName)
        {
            
            var products = new List<ProductToGet>();

            
            using (var client = new HttpClient())
            {
                UriBuilder builder = new UriBuilder($"{URL_API_PRODUCTS}?storeName={storeName}");
                
                var response = await client.GetAsync(builder.Uri);
                if (response.StatusCode != HttpStatusCode.OK)
                    return products;

                return JsonConvert.DeserializeObject<List<ProductToGet>>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<ProductIngredients>> GetProductsIngredients(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
