using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

namespace OrderNow.WebApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientService _httpClientService;

        public OrderService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ResponseDTO?> CreateOrder(CartDTO cartDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = cartDTO,
                ApiUrl = Helpers.OrderAPIBase + "/order/CreateOrder"
            });
        }

        public async Task<ResponseDTO?> CreateStripeSession(StripeRequestDTO stripeRequestDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = stripeRequestDTO,
                ApiUrl = Helpers.OrderAPIBase + "/order/CreateStripeSession"
            });
        }

        public async Task<ResponseDTO?> GetAllOrder(string? userId)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.GET,
                ApiUrl = Helpers.OrderAPIBase + "/order/GetOrders?userId=" + userId
            });
        }

        public async Task<ResponseDTO?> GetOrder(int orderId)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.GET,
                ApiUrl = Helpers.OrderAPIBase + "/order/GetOrder/" + orderId
            });
        }

        public async Task<ResponseDTO?> UpdateOrderStatus(int orderId, string newStatus)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = newStatus,
                ApiUrl = Helpers.OrderAPIBase + "/order/UpdateOrderStatus/" + orderId
            });
        }

        public async Task<ResponseDTO?> ValidateStripeSession(int orderHeaderId)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = orderHeaderId,
                ApiUrl = Helpers.OrderAPIBase + "/order/ValidateStripeSession"
            });
        }
    }
}
