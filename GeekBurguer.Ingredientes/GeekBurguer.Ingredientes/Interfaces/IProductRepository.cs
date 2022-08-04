using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Ingredients>> GetProductsIngredients(Guid productId);

        Task<List<ProductToGet>> GetByStoreName(string storeName);
    }
}
