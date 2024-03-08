using GeekShopping.CartApi.Data.ValueObjects;

namespace GeekShopping.CartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartVo> FindCartByUserId(string userId);
        Task<CartVo> SaveOrUpdateCart(CartVo cart);
        Task<bool> RemoveFromCart(long carDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}