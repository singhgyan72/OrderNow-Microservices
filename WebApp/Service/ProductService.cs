using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

namespace OrderNow.WebApp.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientService _httpClientService;
        public ProductService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ResponseDTO?> CreateProductsAsync(ProductDTO productDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.POST,
                Data = productDTO,
                ApiUrl = Helpers.ProductAPIBase + "/api/product",
                ContentType = Helpers.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDTO?> DeleteProductsAsync(int id)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.DELETE,
                ApiUrl = Helpers.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.GET,
                ApiUrl = Helpers.ProductAPIBase + "/api/product"
            });
        }



        public async Task<ResponseDTO?> GetProductByIdAsync(int id)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.GET,
                ApiUrl = Helpers.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateProductsAsync(ProductDTO productDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO()
            {
                ApiType = Helpers.ApiType.PUT,
                Data = productDTO,
                ApiUrl = Helpers.ProductAPIBase + "/api/product",
                ContentType = Helpers.ContentType.MultipartFormData
            });
        }
    }
}
