using AutoMapper;
using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Ingredientes.Contract.DTOs;

namespace GeekBurguer.Ingredientes.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductToGet, IngredientsResponse>()
                .ForMember(x => x.ProductId, y => y.MapFrom(a => a.ProductId))
                .ForMember(x => x.Ingredients, y => y.MapFrom(a => a.Items.Select(b => b.Name)));
        }
    }
}
