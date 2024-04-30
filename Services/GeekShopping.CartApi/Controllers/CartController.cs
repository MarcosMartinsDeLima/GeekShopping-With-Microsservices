using System.Text.Json;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _repository;

        public CartController(ICartRepository cartRepository)
        {
            _repository = cartRepository;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVo>> FindById(string id)
        {

            var cart = await _repository.FindCartByUserId(id);
            if(cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVo>> AddCart([FromBody]CartVo cartVo)
        {
            var cart = await _repository.SaveOrUpdateCart(cartVo);
            if(cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVo>> UpdateCart(CartVo cartVo)
        {

            var cart = await _repository.SaveOrUpdateCart(cartVo);
            if(cart == null) return NotFound();
            return Ok(cart);
        }


        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVo>> RemoveCart(int id)
        {

            var status = await _repository.RemoveFromCart(id);
            //trocar null por false 
            if(status == false) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVo>> ApplyCoupon([FromBody]CartVo cartVo)
        {
            var status = await _repository.ApplyCoupon(cartVo.CartHeader.UserId, cartVo.CartHeader.CouponCode);
            if(status == false) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{Userid}")]
        public async Task<ActionResult<CartVo>> ApplyCoupon(string Userid)
        {
            var status = await _repository.RemoveCoupon(Userid);
            if(status == false) return NotFound();
            return Ok(status);
        }

    }
}