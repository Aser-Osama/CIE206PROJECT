using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Graphs : PageModel
    {
        private readonly ILogger<Graphs> _logger;

        public Graphs(ILogger<Graphs> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}