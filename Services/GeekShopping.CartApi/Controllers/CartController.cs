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

        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartVo>> AddCart(CartVo cartVo)
        {

            var cart = await _repository.SaveOrUpdateCart(cartVo);
            if(cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
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
            if(status == null) return BadRequest();
            return Ok(status);
        }

    }
}