using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using CIE206PROJECT.Controllers;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class RequestsApproval : PageModel
    {
        public DataTable dt {set; get;}
        
        private readonly DB_Container _DBC;
        private readonly LoginController _LC;
        private RequestsPage _DB;
        private readonly ILogger<RequestsApproval> _logger;

        [BindProperty(SupportsGet =true)]
        public string content { get; set; } 

        public RequestsApproval(ILogger<RequestsApproval> logger, DB_Container container, LoginController _loginController)
        {
            _logger = logger;
            _DBC = container;
            _LC = _loginController;
        }

        public void OnGet()
        {
            _DB = _DBC.requestsPage_DB;
            dt = _DB.getRequests(_LC.GetLoggedInUserId());
        }


    public IActionResult OnPost(int rowIndex, string actionType)
    {
        if (actionType == "accept")
        {
        }
        else if (actionType == "deny")
        {
        }
        Console.WriteLine($"{rowIndex},{actionType}" );
        return RedirectToPage();
    }




    }
}