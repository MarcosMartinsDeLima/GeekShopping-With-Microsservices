using System.Text.Json;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Message;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.RabbitMQSender;
using GeekShopping.CartApi.Repository;
using GeekShopping.CouponApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _repository;
        private ICouponRepository _coupon_repository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository,ICouponRepository couponRepository,IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = cartRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _coupon_repository = couponRepository;
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
        
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout([FromBody]CheckoutHeaderVO vo)
        {
            string token = Request.Headers["Authorization"];
            //verifica se o não esta nulo mas o user id esta nulo
            if(vo?.UserId == null) return BadRequest();
            var cart = await _repository.FindCartByUserId(vo.UserId);
            if(cart == null) return NotFound();
            if(!string.IsNullOrEmpty(vo.CouponCode))
            {
                CouponVo coupon = await _coupon_repository.GetCouponByCouponCode(vo.CouponCode,token);
                if(vo.DiscountTotal != coupon.DiscountAmout)
                {
                    return StatusCode(412);
                }
            }
            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //rabbitmq
            _rabbitMQMessageSender.SendMessage(vo,"checkoutqueue");
            await _repository.ClearCart(vo.UserId);
            return Ok(vo);
        }

    }
}