using GeekBurguer.Ingredientes.Contract.DTOs;

namespace GeekBurguer.Ingredientes.Interfaces
{
    public interface IIngredientsService
    {
        Task<List<IngredientsResponse>> GetProductsByRestrictions(IngredientesRequest request);
    }
}
