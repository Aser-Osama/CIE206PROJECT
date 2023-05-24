using Microsoft.AspNetCore.Mvc.RazorPages;
using CIE206PROJECT.Models;
using System.Collections.Generic;
using CIE206PROJECT.Controllers;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CIE206PROJECT.Pages.Content_Dev
{
    public class content_devModel : PageModel
    {
        private readonly DB_Controller dbController;

        public content_devModel()
        {
            dbController = new DB_Controller();
        }

        public List<lecture> Lectures { get; set; }
        
        public void OnGet()
        {
            Lectures = GetLectures();
        }
        public IActionResult OnPost(lecture newLecture)
        {
            if (ModelState.IsValid)
            {
                AddLecture(newLecture);

                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }
        private List<lecture> GetLectures()
        {
            string query = "SELECT l.lecture_id, l.day, l.room, c.summary_vid " +
                           "FROM lecture AS l " +
                           "INNER JOIN content AS c ON l.content_id = c.content_id";
            DataTable dataTable = dbController.Exec_Queury(query);

            List<lecture> lectures = new List<lecture>();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    lecture lecture = new lecture
                    {
                        lecture_id = Convert.ToInt32(row["lecture_id"]),
                        day = row["day"].ToString(),
                        room = row["room"].ToString(),
                        video_url = row["summary_vid"].ToString()
                    };

                    lectures.Add(lecture);
                }
            }

            return lectures;
        }
        private void AddLecture(lecture newLecture)
        {
            string insertQuery = $"INSERT INTO lecture (lecture_id, day, room, content_id) " +
                                 $"VALUES ({newLecture.lecture_id}, '{newLecture.day}', '{newLecture.room}', 1);";
            dbController.Exec_NonQ(insertQuery);
        }
    }
}
