using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Model.Products>> GetProductsIngredients(Guid productId);

        Task<List<ProductToGet>> GetByStoreName(string storeName);

        bool Add(Model.Products product);

        bool AddRange(IEnumerable<Model.Products> products);

        bool Update(Model.Products product);

        List<Model.Products> GetAll();
        void Delete(Model.Products product);
    }
}
