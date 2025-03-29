using OrderNow.Services.ShoppingCartAPI.Models.DTO;

namespace OrderNow.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
