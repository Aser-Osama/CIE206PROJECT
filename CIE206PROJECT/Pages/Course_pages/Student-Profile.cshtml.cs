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



		public DataTable? Stats { get; set; }
		public DataTable? Courses{ get; set; }
		public DataTable? UserInfo{ get; set; }
		public DataTable? AdditionalUserInfo{ get; set; }
		public DataTable? StudentAttendance{ get; set; }
		public DataTable? PhoneNumbers{ get; set; }
		public DataTable? Notes{ get; set; }

		public CoursePage _DB { get; set; }
        private int id;
		private readonly DB_Container _DBC;
        private readonly LoginController _LC;
		private readonly ILogger<Student_ProfileModel> _logger;


        public Student_ProfileModel(ILogger<Student_ProfileModel> logger, DB_Container container,LoginController Controller_LG){
            _logger = logger;
			_DBC = container;
            _LC=Controller_LG;
            id=_LC.GetLoggedInUserId();
        }

        public void OnGet()
        {
			_DB = _DBC.coursePage_DB;
            UserInfo=_DB.getUserInfo(id);
            if((string)UserInfo.Rows[0][6]=="Student"){
                    StudentAttendance=_DB.getStudentAttendance(id);
                    AdditionalUserInfo=_DB.getStudentInfo(id);
                    Stats=_DB.getStudentEvaluations(id);
                    Courses=_DB.getGroupsStudent(id);
                    PhoneNumbers=_DB.getUserPhonenumbers(id);
                    Notes=_DB.getStudentsNotes(id);
            }
            else if ((string)UserInfo.Rows[0][6]=="Trainer"){
                    AdditionalUserInfo=_DB.getTrainerInfo(id);
                    Stats=_DB.getTrainerEvaluations(id);
                    Courses=_DB.getGroupsTrainer(id);
                    PhoneNumbers=_DB.getUserPhonenumbers(id);
                    Notes=_DB.getStudentsNotes(id);
            }
            else if ((string)UserInfo.Rows[0][6]=="Parent"){
                    StudentAttendance=_DB.getStudentAttendance(id);
                    int id_s=(int)_DB.getStudentId(id);
                    AdditionalUserInfo=_DB.getStudentInfo(id_s);
                    Stats=_DB.getStudentEvaluations(id_s);
                    Courses=_DB.getGroupsStudent(id_s);
                    PhoneNumbers=_DB.getUserPhonenumbers(id_s);
                    Notes=_DB.getStudentsNotes(id);
            }



        }
    }
}
