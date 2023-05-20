using CIE206PROJECT.Controllers;
using CIE206PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CIE206PROJECT.Models.UserTypes;

namespace CIE206PROJECT.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private readonly LoginController loginController;

        public IndexModel(ILogger<IndexModel> logger, LoginController _loginController)
		{
			_logger = logger;
            loginController = _loginController;
            Email = string.Empty;
            Password = string.Empty;
        }
        public bool isLoggedin()
        {
            return loginController.IsLoggedIn();
        }
        public IActionResult OnPostLogin()
        {
            if (loginController.Login(Email, Password)) 
            {
                Console.WriteLine("Successfull login");
                if (loginController.IsAdmin())
                {
                    Console.WriteLine("Logged in as admin");
                    return RedirectToPage("/Admin_Pages/Data");
                }
                else
                {
                    Console.WriteLine("Logged in as User");
                    int ID = loginController.GetLoggedInUserId();
                    return RedirectToPage("/Course_pages/Group-Page");
                }
            }
            else
            {
                Console.WriteLine("Log in Failed");
                return Page();
            }
		}

        public IActionResult OnPostLogout()
        {
            Console.WriteLine("Logged out");
            loginController.Logout();
            return Page();
        }
    }
}