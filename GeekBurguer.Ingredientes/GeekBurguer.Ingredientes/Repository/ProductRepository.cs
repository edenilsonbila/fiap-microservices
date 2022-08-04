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
        private const string URL_API_PRODUCTS = "http://localhost:54972/api/products";
        private readonly ProductsDbContext _context;
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

        public async Task<IEnumerable<Ingredients>> GetProductsIngredients(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
