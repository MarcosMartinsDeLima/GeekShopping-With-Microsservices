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

        public CartController(IProductService productService,ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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
                    foreach(var item in response.CartDetails)
                    {
                        response.CartHeader.PurchaseAmout+= item.Product.Price * item.Count;
                    }
                }

                return response;
            }
        }
}