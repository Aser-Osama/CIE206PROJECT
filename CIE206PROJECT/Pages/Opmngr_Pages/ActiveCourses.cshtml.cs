using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Opmngr_Pages
{
    public class ActiveCourses : PageModel
    {
        private readonly ILogger<ActiveCourses> _logger;

        public ActiveCourses(ILogger<ActiveCourses> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}