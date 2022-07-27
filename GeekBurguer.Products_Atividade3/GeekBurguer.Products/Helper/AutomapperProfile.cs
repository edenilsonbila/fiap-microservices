using AutoMapper;
using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Products.Contract.DTO;
using GeekBurguer.Products.Models;

namespace GeekBurguer.Products.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductToGet>();
            CreateMap<Item, ItemToGet>();
            CreateMap<ProductToUpsert, Product>()
    .AfterMap<MatchStoreFromRepository>();
            CreateMap<ItemToUpsert, Item>()
                .AfterMap<MatchItemsFromRepository>();

        }
    }

}
