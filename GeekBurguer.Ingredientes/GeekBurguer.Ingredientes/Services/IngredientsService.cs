using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.Interfaces;

namespace GeekBurguer.Ingredientes.Services
{
    public class IngredientsService : IIngredientsService
    {
        public Task<List<IngredientsResponse>> GetProductsByRestrictions(IngredientesRequest request)
        {
            return Task.FromResult(new List<IngredientsResponse>());
        }
    }
}
