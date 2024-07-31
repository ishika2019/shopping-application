using AutoMapper;
using project.Controllers;
using project.DTO;
using project.Entities;
using project.Entities.identity;

namespace project.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ForMember(p => p.productbrand, o => o.MapFrom(p => p.productbrand.Name))
                .ForMember(p => p.producttype, o => o.MapFrom(p => p.producttype.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

        }
    }
}
