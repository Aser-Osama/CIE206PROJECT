using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using CIE206PROJECT.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;


namespace CIE206PROJECT.Pages.Course_pages
{
    public class Student_overviewModel : PageModel
    {

		public DataTable? Stats { get; set; }
		public DataTable? UserInfo{ get; set; }
		public DataTable? AdditionalUserInfo{ get; set; }
		public DataTable? PhoneNumbers{ get; set; }
		public DataTable? StudentAttendance{ get; set; }
		public CoursePage _DB { get; set; }
        private int id;
		private readonly DB_Container _DBC;
        private readonly LoginController _LC;
		private readonly ILogger<Student_ProfileModel> _logger;

        public Student_overviewModel(ILogger<Student_ProfileModel> logger, DB_Container container,LoginController Controller_LG){
            _logger = logger;
			_DBC = container;
            _LC=Controller_LG;
            id=_LC.GetLoggedInUserId();
        }

        public void OnGet()
        {
        }
    }
}
