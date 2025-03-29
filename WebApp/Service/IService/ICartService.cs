using OrderNow.WebApp.Models;

namespace OrderNow.WebApp.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDTO?> GetCartByUserIdAsnyc(string userId);
        Task<ResponseDTO?> UpsertCartAsync(CartDTO cartDTO);
        Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDTO?> ApplyCouponAsync(CartDTO cartDTO);
        Task<ResponseDTO?> EmailCart(CartDTO cartDTO);
    }
}
