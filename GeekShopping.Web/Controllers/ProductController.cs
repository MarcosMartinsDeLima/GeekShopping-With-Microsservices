using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost("/update")]
        public async Task<IActionResult> ProductUpdatePost(ProductModel model)
        {   
            if(ModelState.IsValid){
                var response = await _productService.Update(model);
                if(response != null) return RedirectToAction(nameof(ProductIndex));
            }
            
            return View();
        }

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
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {   
           
                var response = await _productService.Delete(model.Id);
                return RedirectToAction(nameof(ProductIndex));
        }

    }
}