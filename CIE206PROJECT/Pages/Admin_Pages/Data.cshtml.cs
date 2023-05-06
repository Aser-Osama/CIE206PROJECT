using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Data : PageModel
    {
        private readonly ILogger<Data> _logger;

        public Data(ILogger<Data> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}