using AutoMapper;
using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.Model;

namespace GeekBurguer.Ingredientes.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductToGet, IngredientsResponse>()
                .ForMember(x => x.ProductId, y => y.MapFrom(a => a.ProductId))
                .ForMember(x => x.Ingredients, y => y.MapFrom(a => a.Items.Select(b => b.Name)));
            CreateMap<ProductToGet, Model.Products>()
                .ForMember(x => x.ProductId, y => y.MapFrom(a => a.ProductId))
                .ForMember(x => x.Items, y => y.MapFrom(a => a.Items.Select<ItemToGet, string>(b => b.Name)));
        }
    }
}
