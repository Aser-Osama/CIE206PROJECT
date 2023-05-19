using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CIE206PROJECT.Controllers
{
    public class Login_Controller : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Login_Controller(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        
    }
}
