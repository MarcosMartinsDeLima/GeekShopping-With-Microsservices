using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GeekShopping.Web.Views.Cart
{
    public class CartIndex : PageModel
    {
        private readonly ILogger<CartIndex> _logger;

        public CartIndex(ILogger<CartIndex> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}