using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CIE206PROJECT.Controllers;
using CIE206PROJECT.Pages.Admin_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIE206PROJECT.Pages.STEM_svpages
{
    public class StemsvModel : PageModel
    {
        public DataTable dt { get; set; }
        private readonly DB_Controller _dbController;

        private readonly ILogger<Data> _logger;

        public StemsvModel(ILogger<Data> logger)
        {
            _logger = logger;
            _dbController = new DB_Controller();
        }

        public void OnGet()
        {
            DB_Controller dbController = new DB_Controller();
            DataTable groupData = dbController.Exec_Queury("SELECT * FROM Student_groups");
            string html = "";
            foreach (DataRow row in groupData.Rows)
            {
                int groupNo = Convert.ToInt32(row["group_no"]);
                int studentId = Convert.ToInt32(row["Student_id"]);
                string groupName = $"Group {groupNo}";
                string cardHtml = @"
                <div class='col-md-4'>
                  <div class='card'>
                    <div class='card-body'>
                      <h5 class='card-title'>Group: " + groupName + @"</h5>
                      <p class='card-text'>Group Members:</p>
                      <ul class='list-group'>
                        <li class='list-group-item'>Student ID: " + studentId + @"</li>
                      </ul>
                    </div>
                  </div>
                </div>";

                html += cardHtml;
            }
            ViewData["GroupsHtml"] = html;
        }
        public IActionResult OnPost(int groupNumber, int studentId)
        {

            string insertQuery = $"INSERT INTO Student_groups (group_no, Student_id) VALUES ({groupNumber}, {studentId})";
            _dbController.Exec_NonQ(insertQuery);

            return RedirectToPage("./Stemsv");
        }
    }
}
