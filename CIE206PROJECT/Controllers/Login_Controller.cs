using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace CIE206PROJECT.Controllers
{
    public class LoginController : ControllerBase
    {
        DB_Controller database = new DB_Controller();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Login(string Email, string Password)
        {
            string q = $"select [user_ID], User_type, [Password] from [user] where Email = '{Email}'";
            DataTable? dataTable = database.Exec_Queury(q);
            if (dataTable is null || dataTable.Rows.Count == 0)
            {
                return false;
            }
            else if ((string)dataTable.Rows[0]["Password"] != Password)
            {
                return false;
            }
            else
            {
                int id = (int)dataTable.Rows[0]["user_ID"];
                string userType = (string)dataTable.Rows[0]["User_type"];
                _httpContextAccessor.HttpContext.Session.SetString("logged_in", "true");
                _httpContextAccessor.HttpContext.Session.SetInt32("user_id", id);
                _httpContextAccessor.HttpContext.Session.SetString("user_type", userType);
                return true;
            }

        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            _httpContextAccessor.HttpContext.Session.SetString("logged_in", "false");
        }

        public bool IsLoggedIn() //true n false
        {
            string? isLoggedIn = _httpContextAccessor.HttpContext.Session.GetString("logged_in");
            if (isLoggedIn == null)
            { return false; }

            return (isLoggedIn == "true");
        }

        public int GetLoggedInUserId() //returns -1 if no user is saved/logged in
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("user_id") ?? -1;
        }

        public bool IsAdmin()
        {
            string? userType = _httpContextAccessor.HttpContext.Session.GetString("user_type");
            if (userType == null) { return false; }

            return (userType == "CEO" || userType == "op_mngr" || userType == "senior_supervisor" || userType == "coordinator");
        }
    }
}
