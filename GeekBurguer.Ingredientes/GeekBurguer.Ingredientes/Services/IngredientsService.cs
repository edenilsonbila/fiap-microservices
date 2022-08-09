using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Model;

namespace GeekBurguer.Ingredientes.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IProductRepository _productRepository;
        public IngredientsService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<IngredientsResponse>> GetProductsByRestrictions(IngredientsRequest request)
        {
            List<IngredientsResponse> prodIngredients = new();
            var products = await _productRepository.GetByStoreName(request.StoreName);

            foreach (var product in products)
            {
                foreach (var restriction in request.Restrictions)
                {
                    if (product.Items.FirstOrDefault(e => e.Name.Contains(restriction)) == null)
                    {
                        var ingredient = new IngredientsResponse();
                        ingredient.ProductId = product.ProductId;
                        ingredient.Ingredients = new List<string>();

                        foreach (var item in product.Items)
                        {
                            ingredient.Ingredients.Add(item.Name);
                        }

                        if (!prodIngredients.Contains(ingredient))
                            prodIngredients.Add(ingredient);
                    }
                }
            }

            return prodIngredients;
        }
    }
}
