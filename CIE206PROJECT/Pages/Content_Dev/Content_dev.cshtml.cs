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
        public List<Course> Courses { get; set; }

        public void OnGet()
        {
            Lectures = GetLectures();
            Courses = GetCourses();
        }
        public IActionResult OnPost(int lectureId, int contentId, int groupId, DateTime day, string room)
        {
            try
            {
                // Perform any necessary validation or checks before adding the lecture

                // Create a new lecture object
                lecture newLecture = new lecture
                {
                    lecture_id = lectureId,
                    content_id = contentId,
                    group_id = groupId,
                    day = day,
                    room = room
                };

                // Add the lecture to the database
                AddLecture(newLecture);

                return RedirectToPage("/Content_Dev/content_dev");
            }
            catch (Exception ex)
            {
                // Handle the exception and display an error message to the user
                ModelState.AddModelError(string.Empty, ex.Message);
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
                        day = Convert.ToDateTime(row["day"]),
                        room = row["room"].ToString(),
                        video_url = row["summary_vid"].ToString()
                    };

                    lectures.Add(lecture);
                }
            }

            return lectures;
        }
        private List<Course> GetCourses()
        {
            string query = "SELECT * FROM course";
            DataTable dataTable = dbController.Exec_Queury(query);

            List<Course> courses = new List<Course>();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Course course = new Course
                    {
                        course_id = Convert.ToInt32(row["course_id"]),
                        course_name = row["course_name"].ToString(),
                        course_description = row["course_description"].ToString(),
                        tot_sessions = Convert.ToInt32(row["tot_sessions"]),
                        advertisement_text = row["advertisement_text"].ToString(),
                        video_link = row["video_link"].ToString()
                    };

                    courses.Add(course);
                }
            }

            return courses;
        }
        public IActionResult OnPostAddContent(int contentId, int courseId, string summary, string summaryVideoUrl, string slidesUrl, string teacherGuideUrl, string handoutUrl)
        {
            try
            {
                // Perform any necessary validation or checks before adding the content

                // Create a new content object
                Content newContent = new Content
                {
                    content_id = contentId,
                    course_id = courseId,
                    summary = summary,
                    summary_vid = summaryVideoUrl,
                    slides = slidesUrl,
                    teacher_guide = teacherGuideUrl,
                    handout = handoutUrl
                };

                // Add the content to the database
                AddContent(newContent);

                return RedirectToPage("/Content_Dev/content_dev");
            }
            catch (Exception ex)
            {
                // Handle the exception and display an error message to the user
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        //private void AddLecture(lecture newLecture)
        //{
        //    string insertQuery = $"INSERT INTO lecture (lecture_id, day, room, content_id) " +
        //                         $"VALUES ({newLecture.lecture_id}, '{newLecture.day}', '{newLecture.room}', 1);";
        //    dbController.Exec_NonQ(insertQuery);
        //}
        private void AddLecture(lecture newLecture)
        {
            string insertQuery = $"INSERT INTO lecture (lecture_id, day, room, content_id) " +
                     $"VALUES ({newLecture.lecture_id}, '{newLecture.day.ToString("yyyy-MM-dd")}', '{newLecture.room}', " +
                     $"(SELECT content_id FROM content WHERE content_id = {newLecture.content_id}));";
            dbController.Exec_NonQ(insertQuery);

        }
        private void AddContent(Content newContent)
        {
            string insertQuery = $"INSERT INTO content (content_id, course_id, summary, summary_vid, slides, teacher_guide, handout) " +
                                 $"VALUES ({newContent.content_id}, {newContent.course_id}, '{newContent.summary}', " +
                                 $"'{newContent.summary_vid}', '{newContent.slides}', '{newContent.teacher_guide}', '{newContent.handout}')";

            dbController.Exec_NonQ(insertQuery);
        }


    }
}
