using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Model;

namespace GeekBurguer.Ingredientes.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        public Task<List<ProductToGet>> GetByStoreName(string storeName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ingredients>> GetProductsIngredients(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
