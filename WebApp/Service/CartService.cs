using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

namespace OrderNow.WebApp.Service
{
    public class CartService : ICartService
    {
        private readonly IHttpClientService _httpClientService;

        public CartService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ResponseDTO?> ApplyCouponAsync(CartDTO cartDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = cartDTO,
                ApiUrl = Helpers.ShoppingCartAPIBase + "/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDTO?> EmailCart(CartDTO cartDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = cartDTO,
                ApiUrl = Helpers.ShoppingCartAPIBase + "/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseDTO?> GetCartByUserIdAsnyc(string userId)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.GET,
                ApiUrl = Helpers.ShoppingCartAPIBase + "/cart/GetCart/" + userId
            });
        }


        public async Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = cartDetailsId,
                ApiUrl = Helpers.ShoppingCartAPIBase + "/cart/RemoveCart"
            });
        }


        public async Task<ResponseDTO?> UpsertCartAsync(CartDTO cartDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = cartDTO,
                ApiUrl = Helpers.ShoppingCartAPIBase + "/cart/CartUpsert"
            });
        }
    }
}
