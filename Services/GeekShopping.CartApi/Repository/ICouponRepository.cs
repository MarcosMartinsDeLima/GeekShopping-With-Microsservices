using GeekShopping.CartApi.Data.ValueObjects;

namespace GeekShopping.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVo> GetCouponByCouponCode(string couponCode,string token);
    }
}