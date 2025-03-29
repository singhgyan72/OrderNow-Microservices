
using OrderNow.Services.OrderAPI.Models.DTO;

namespace OrderNow.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
