using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GeekShopping.Web.Views.Cart
{
    public class Confirmation : PageModel
    {
        private readonly ILogger<Confirmation> _logger;

        public Confirmation(ILogger<Confirmation> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}