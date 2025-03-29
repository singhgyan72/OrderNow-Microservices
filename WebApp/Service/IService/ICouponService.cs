using OrderNow.WebApp.Models;

namespace OrderNow.WebApp.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO> GetAllCouponsAsync();

        Task<ResponseDTO> GetCouponAsync(string couponCode);

        Task<ResponseDTO> GetCouponByIdAsync(int couponId);

        Task<ResponseDTO> CreateCouponAsync(CouponDTO coupon);

        Task<ResponseDTO> UpdateCouponAsync(CouponDTO coupon);

        Task<ResponseDTO> DeleteCouponAsync(int couponId);
    }
}
