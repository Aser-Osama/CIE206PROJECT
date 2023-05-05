using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Opmngr_Pages
{
    public class Course_Edit : PageModel
    {
        private readonly ILogger<Course_Edit> _logger;

        public Course_Edit(ILogger<Course_Edit> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}