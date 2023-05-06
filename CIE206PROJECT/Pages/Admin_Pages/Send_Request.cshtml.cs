using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Send_Request : PageModel
    {
        private readonly ILogger<Send_Request> _logger;

        public Send_Request(ILogger<Send_Request> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}