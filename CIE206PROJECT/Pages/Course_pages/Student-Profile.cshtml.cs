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




namespace CIE206PROJECT.Pages
{
    public class Student_ProfileModel : PageModel
    {



		public DataTable Stats { get; set; }
		public DataTable Courses{ get; set; }
		public DataTable UserInfo{ get; set; }
		public DataTable PhoneNumbers{ get; set; }
		public DataTable Notes{ get; set; }

		public CoursePage _DB { get; set; }
        private int id;
		private readonly DB_Container _DBC;
		private readonly ILogger<Student_ProfileModel> _logger;


        public Student_ProfileModel(ILogger<Student_ProfileModel> logger, DB_Container container){


            _logger = logger;
			_DBC = container;

        }
        public void OnGet()
        {
			_DB = _DBC.coursePage_DB;
            Stats=_DB.getStudentEvaluations(id);
            Courses=_DB.getGroupsStudent(id);
            UserInfo=_DB.getStudentInfo(id);
            PhoneNumbers=_DB.getUserPhonenumbers(id);
            Notes=_DB.getStudentsNotes(id);
        }
    }
}
