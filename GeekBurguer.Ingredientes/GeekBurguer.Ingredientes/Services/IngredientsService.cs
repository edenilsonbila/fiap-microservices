using AutoMapper;
using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Model;

namespace GeekBurguer.Ingredientes.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;
        public IngredientsService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<IngredientsResponse>> GetProductsByRestrictions(IngredientsRequest request)
        {
            List<IngredientsResponse> prodIngredients = new();
            var products = _productRepository.GetAll();

            foreach (var product in products)
            {
                foreach (var restriction in request.Restrictions)
                {
                    var prodsOk = products.Where(e => !e.Ingredients.Contains(restriction));

                    //if (product.Ingredients.FirstOrDefault(e => e.Name.Contains(restriction)) == null)
                    //{
                    var ingredient = new IngredientsResponse();
                    ingredient.ProductId = product.ProductId;
                    ingredient.Ingredients = product.Ingredients;

                    //foreach (var item in product.Items)
                    //{
                    //    ingredient.Ingredients.Add(item.Name);
                    //}

                    if (!prodIngredients.Contains(ingredient))
                        prodIngredients.Add(ingredient);
                    //}
                }
            }

            return prodIngredients;
        }

        public async Task GetProducts()
        {
            try
            {


                var products = await _productRepository.GetByStoreName("paulista");

                var productsToGet = _mapper.Map<IEnumerable<ProductIngredients>>(products);

                if (productsToGet.Any())
                {
                    _productRepository.AddRange(productsToGet);
                }

                //Depois implementar o Merge - Falta esse
                //E o ProductChanged

                return;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
