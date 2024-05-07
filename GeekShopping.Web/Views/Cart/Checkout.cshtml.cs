using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GeekShopping.Web.Views.Cart
{
    public class Checkout : PageModel
    {
        private readonly ILogger<Checkout> _logger;

        public Checkout(ILogger<Checkout> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}