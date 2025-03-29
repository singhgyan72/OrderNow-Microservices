using AutoMapper;
using OrderNow.Services.ShoppingCartAPI.Models.DTO;
using OrderNow.Services.ShoppingCartAPI.Models;

namespace OrderNow.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
