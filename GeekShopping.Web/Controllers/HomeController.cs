using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using GeekShopping.Web.Services.Iservices;

namespace GeekShopping.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var acessToken = await HttpContext.GetTokenAsync("access_token");
        var products = await _productService.FindAll(acessToken);
        return View(products);
      
    }

    [Authorize]
       public async Task<IActionResult> Details(int id)
    {
        var acessToken = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindById(id,acessToken);
        return View(model);
      
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var acessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(IndexAsync));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies","oidc");
    }
}
