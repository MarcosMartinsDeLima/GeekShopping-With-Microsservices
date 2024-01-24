using GeekShopping.Services.ProductApi.Data.ValueObject;
using GeekShopping.Services.ProductApi.Model.Repository;
using GeekShopping.Services.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Services.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository){
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll(){
            var product = await _repository.FindAll();
            return Ok(product);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult <ProductVO>> FindById(long id){
            
            var product = await _repository.FindById(id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
         public async Task<ActionResult <ProductVO>> Create([FromBody]ProductVO vo){
            if(vo == null) return BadRequest();
            var product = await _repository.Create(vo);
            Response.StatusCode = 201;
            return new JsonResult(product);
        }

        [HttpPut]
        [Authorize]
         public async Task<ActionResult <ProductVO>> Update([FromBody]ProductVO vo){
            if(vo == null) return BadRequest();
            var product = await _repository.Update(vo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<ActionResult> Delete(long Id){
            var status = await _repository.Delete(Id);
            if(!status) return BadRequest();
            return Ok(status);
            
        }
    }
}