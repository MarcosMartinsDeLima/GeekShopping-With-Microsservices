using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Iservices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
            private readonly IProductService _productService;

        public ProductController( IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {   
            var products = await _productService.FindAll();
            return View(products);
        }

        [HttpGet("/create")]
           public async Task<IActionResult> ProductCreate()
        {   
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreatePost(ProductModel model)
        {   
            if(ModelState.IsValid){
                var response = await _productService.Create(model);
                if(response != null) return RedirectToAction(nameof(ProductIndex));
            }
            
            return View();
        }

        [HttpGet("/update")]
        public async Task<IActionResult> ProductUpdate(long id)
        {   
            var product = _productService.FindById(id);
            ProductModel productModel = new ProductModel();
            productModel.Name = product.Result.Name;
            productModel.Id = product.Result.Id;
            productModel.Price = Convert.ToDecimal(product.Result.Price);
            productModel.Description = product.Result.Description;
            productModel.Category_name = product.Result.Category_name;
            productModel.Image_url = product.Result.Image_url;
            if(product != null) return View(productModel);
            else return NotFound();
        }

        [Authorize]
        [HttpPost("/update")]
        public async Task<IActionResult> ProductUpdatePost(ProductModel model)
        {   
            if(ModelState.IsValid){
                var response = await _productService.Update(model);
                if(response != null) return RedirectToAction(nameof(ProductIndex));
            }
            
            return View();
        }
        
        [Authorize]
        [HttpGet("/delete")]
        public async Task<IActionResult> ProductDelete(long id)
        {   
            var product = _productService.FindById(id);
            ProductModel productModel = new ProductModel();
            productModel.Name = product.Result.Name;
            productModel.Id = product.Result.Id;
            productModel.Price = Convert.ToDecimal(product.Result.Price);
            productModel.Description = product.Result.Description;
            productModel.Category_name = product.Result.Category_name;
            productModel.Image_url = product.Result.Image_url;
            if(product != null) return View(productModel);
            else return NotFound();
        }

        [HttpPost("/delete")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {   
           
                var response = await _productService.Delete(model.Id);
                return RedirectToAction(nameof(ProductIndex));
        }

    }
}