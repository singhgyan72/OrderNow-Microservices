using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

namespace OrderNow.WebApp.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientService _httpClientService;

        public CouponService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ResponseDTO> CreateCouponAsync(CouponDTO coupon)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.POST,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI",
                Data = coupon
            });
        }

        public async Task<ResponseDTO> DeleteCouponAsync(int couponId)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.DELETE,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI/" + couponId
            });
        }

        public async Task<ResponseDTO> GetAllCouponsAsync()
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.GET,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI"
            });
        }

        public async Task<ResponseDTO> GetCouponAsync(string couponCode)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.GET,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDTO> GetCouponByIdAsync(int couponId)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.GET,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI/" + couponId
            });
        }

        public async Task<ResponseDTO> UpdateCouponAsync(CouponDTO coupon)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.PUT,
                ApiUrl = Helpers.CouponAPIBase + "/couponAPI",
                Data = coupon
            });
        }
    }
}
