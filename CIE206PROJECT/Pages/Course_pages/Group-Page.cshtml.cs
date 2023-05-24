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
    public class Group_PageModel : PageModel
    {
        public DataTable? GroupContent { get; set; }
        public DataTable? Students { get; set; }
        public DataTable? UpcomingLecture { get; set; }
        public DataTable? GroupInfo { get; set; }
        public List<DataTable?> ContentTopics { get; set; }
        public DataTable? UpcomingLectureTopics { get; set; }
        public int? StudentCount { get; set; }
        public string UserType { get; set; } // Added property to store user type

        public CoursePage _DB { get; set; }
        private int user_id;
        private readonly DB_Container _DBC;
        private readonly LoginController _LC;
        private readonly ILogger<Student_ProfileModel> _logger;

        public Group_PageModel(ILogger<Student_ProfileModel> logger, DB_Container container, LoginController Controller_LG)
        {
            _logger = logger;
            _DBC = container;
            _LC = Controller_LG;
            user_id = _LC.GetLoggedInUserId();
            ContentTopics = new List<DataTable?>(); // Initialize ContentTopics list
        }

        public void OnGet()
        {
			int id = _LC.GetLoggedInUserId();
			_DB = _DBC.coursePage_DB;
            GroupContent = _DB.getGroupContent(id);
            Students = _DB.getStudentsGroup(id);
            StudentCount = _DB.getStudentCount(id);
            UpcomingLecture = _DB.getUpcomingLecture(id);
            UpcomingLectureTopics = _DB.getGroupContentTopics(id);
            GroupInfo = _DB.getGroupInfo(id);
            for (int i = 0; GroupContent.Rows.Count > i; i++)
            {
                ContentTopics.Append(_DB.getGroupContentTopics(i));
            }

            // Retrieve user type from the LoginController
            UserType = (string)_DB.getUserInfo(user_id).Rows[0][6];
        }
    }
}
