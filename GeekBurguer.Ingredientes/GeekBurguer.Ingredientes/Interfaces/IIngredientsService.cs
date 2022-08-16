using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.DTO;

namespace GeekBurguer.Ingredientes.Interfaces
{
    public interface IIngredientsService
    {
        Task<List<IngredientsResponse>> GetProductsByRestrictions(IngredientsRequest request);

        Task GetProducts();

        void MargeProductsAndIngredients(LabelImageDTO labelImageDto);
    }
}
