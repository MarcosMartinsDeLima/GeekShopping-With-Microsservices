using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Iservices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService,ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

            [HttpGet("Index")] 
            [Authorize]
            public async Task<IActionResult> CartIndex()
            {
                return View(await FindUserCart());
            }

            [HttpPost("ApplyCoupon")]
            [ActionName("ApplyCoupon")] 
            [Authorize]
            public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _cartService.ApplyCoupon(cartViewModel, acessToken);
                if (response)
                {
                    return RedirectToAction(nameof(CartIndex));
                }

                return View();
            }

            [HttpPost("RemoveCoupon")]
            [ActionName("RemoveCoupon")] 
            [Authorize]
            public async Task<IActionResult> RemoveCoupon()
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");
                var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
                var response = await _cartService.RemoveCoupon(userId, acessToken);
                if (response)
                {
                    return RedirectToAction(nameof(CartIndex));
                }

                return View();
            }

            public async Task<IActionResult> Remove(int id)
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.RemoveFromCart(id, acessToken);
                if (response)
                {
                    return RedirectToAction(nameof(CartIndex));
                }

                return View();
            }

            private async Task<CartViewModel> FindUserCart()
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");
                var UserId =  User.Claims.Where(u => u.Type =="sub")?.FirstOrDefault()?.Value;

                var response = await _cartService.FindCartByUserId(UserId, acessToken); 
                if(response?.CartHeader != null)
                {
                    if(!string.IsNullOrEmpty(response.CartHeader.CouponCode))
                    {
                        var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode,acessToken);
                        if(coupon?.CouponCode != null)
                        {
                            response.CartHeader.DiscountTotal = coupon.DiscountAmout;
                        }
                    }

                    foreach(var item in response.CartDetails)
                    {
                        response.CartHeader.PurchaseAmout+= item.Product.Price * item.Count;
                    }

                    response.CartHeader.PurchaseAmout -= response.CartHeader.DiscountTotal;
                }

                return response;
            }
            
            [HttpGet]
             public async Task<IActionResult> Checkout()
            {
                return View(await FindUserCart());
            }

            [HttpPost]
            public async Task<IActionResult> Checkout(CartViewModel model)            
            {
                var acessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.Checkout(model.CartHeader, acessToken);
                
                if(response != null)
                {
                    RedirectToAction(nameof(Confirmation));
                }

                return View(model);   
            }

            [HttpGet]
            public async Task<IActionResult> Confirmation()            
            {
                return View();   
            }
        }
}