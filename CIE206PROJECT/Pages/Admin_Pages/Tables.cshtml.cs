using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Tables : PageModel
    {
        private readonly ILogger<Tables> _logger;

        public Tables(ILogger<Tables> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}