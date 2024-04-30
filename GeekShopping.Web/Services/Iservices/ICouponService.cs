using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Iservices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}