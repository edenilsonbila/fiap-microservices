using AutoMapper;
using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.DTO;
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
                var prodHasRestriction = true;

                if (product.Items.All(e => e.Ingredients == null))
                {
                    prodHasRestriction = false;
                }
                else
                {
                    foreach (var item in product.Items)
                    {
                        //valida se os igredientes possui alguma das restriçoes enviadas
                        if (item.Ingredients != null && !item.Ingredients.Any(e => request.Restrictions.Contains(e)))
                        {
                            prodHasRestriction = false;
                            break;
                        }
                    }
                }
                //FINALIZAR AQ PQ QUEBRANDO A LOGICA
                if (!prodHasRestriction)
                {
                    var ingredient = new IngredientsResponse();
                    ingredient.ProductId = product.ProductId;

                    ingredient.Ingredients = product.Items.Select(e => e.Ingredients).ToArray()[0];

                    if (!prodIngredients.Contains(ingredient))
                        prodIngredients.Add(ingredient);
                }
            }

            return prodIngredients;
        }

        public async Task GetProducts()
        {
            //MOCK
            _productRepository.Add(new Model.Products()
            {
                StoreName = "paulista",
                ProductId = new Guid(),
                Items = new List<ItemIngredients> {
                new ItemIngredients() { ItemId = new Guid(), Name = "meat", Ingredients = { "fiber","salt" } },
                new ItemIngredients() { ItemId = new Guid(), Name = "bread", Ingredients = { "gluten", "suggar","egg" } },
                new ItemIngredients() { ItemId = new Guid(), Name = "lettuce", Ingredients = { "chlorophyll" } },
            }
            });
            _productRepository.Add(new Model.Products()
            {
                StoreName = "paulista",
                ProductId = new Guid(),
                Items = new List<ItemIngredients> {
                new ItemIngredients() { ItemId = new Guid(), Name = "cheese", Ingredients = { "lactose", "fat","dye" } },
                new ItemIngredients() { ItemId = new Guid(), Name = "tomato", Ingredients = { "chlorophyll" } },
                new ItemIngredients() { ItemId = new Guid(), Name = "bacon", Ingredients = { "fat","dye" } },
            }
            });


            var products = await _productRepository.GetByStoreName("paulista");

            var productsToGet = _mapper.Map<IEnumerable<Model.Products>>(products);

            if (productsToGet.Any())
            {
                _productRepository.AddRange(productsToGet);
            }
        }

        public void MargeProductsAndIngredients(LabelImageDTO labelImageDto)
        {
            //procura os produtos que possui este item: Carne
            var produtos = _productRepository.GetAll().Where(e => e.Items.Any(x => x.Name == labelImageDto.ItemName)).ToList();

            foreach (var produto in produtos)
            {
                //Atualiza os itens com os ingredients enviados
                foreach (var item in produto.Items.Where(e => e.Name == labelImageDto.ItemName))
                {
                    item.Ingredients = labelImageDto.Ingredients;
                }
            }
        }
    }
}
