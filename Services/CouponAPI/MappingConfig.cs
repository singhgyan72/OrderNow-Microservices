using AutoMapper;
using OrderNow.Services.CouponAPI.Models;
using OrderNow.Services.CouponAPI.Models.DTO;

namespace OrderNow.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDTO>();
                config.CreateMap<CouponDTO, Coupon>();
            });
            return mappingConfig;
        }
    }
}
