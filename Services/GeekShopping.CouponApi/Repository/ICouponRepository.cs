using GeekShopping.CouponApi.Data.ValueObjects;

namespace GeekShopping.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVo> GetCouponByCouponCode(string couponCode);
    }
}