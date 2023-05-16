using CIE206PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CIE206PROJECT.Models.UserTypes;

namespace CIE206PROJECT.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, UserTypes userTypes)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}
	}
}