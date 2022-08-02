using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Model;

namespace GeekBurguer.Ingredientes.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Ingredients>> GetProductsIngredients(Guid productId);

        Task<List<ProductToGet>> GetByStoreName(string storeName);
    }
}
