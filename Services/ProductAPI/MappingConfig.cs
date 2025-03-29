using AutoMapper;
using OrderNow.Services.ProductAPI.Models;
using OrderNow.Services.ProductAPI.Models.DTO;

namespace OrderNow.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
