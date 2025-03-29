using OrderNow.Services.ShoppingCartAPI.Models.DTO;
using OrderNow.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace OrderNow.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDTO> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/couponAPI/GetByCode/{couponCode}");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContet);
            if (resp!=null && resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(resp.Result));
            }
            return new CouponDTO();
        }
    }
}
