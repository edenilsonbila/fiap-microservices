using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace GeekBurguer.Ingredientes.Repository
{
    public class ProductRepository : IProductRepository
    {
        private const string URL_API_PRODUCTS = "https://geekburger-products.azurewebsites.net/api/products";//"http://localhost:54972/api/products";
        private readonly List<Model.Products> _db;


        public ProductRepository()
        {
            _db = new List<Model.Products>();
        }


        public bool Add(Model.Products product)
        {

            _db.Add(product);
            return true;
        }

        public bool AddRange(IEnumerable<Model.Products> products)
        {
            _db.AddRange(products);
            /*
            _context.ProductIgredients.AddRange(products);
            _context.SaveChanges();*/
            return true;
        }

        public List<Model.Products> GetAll()
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

        public async Task<IEnumerable<Model.Products>> GetProductsIngredients(Guid productId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Model.Products product)
        {
            var index = _db.IndexOf(product);
            _db[index] = product;
            return true;
        }
    }
}
