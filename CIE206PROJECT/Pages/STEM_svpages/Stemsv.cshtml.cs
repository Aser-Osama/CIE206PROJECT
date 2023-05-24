using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CIE206PROJECT.Controllers;
using CIE206PROJECT.Pages.Admin_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.STEM_svpages
{
    public class StemsvModel : PageModel
    {
        public string GroupsHtml { get; set; }
        public string TrainersHtml { get; set; }
        private readonly DB_Controller _dbController;
        private readonly ILogger<StemsvModel> _logger;

        public StemsvModel(ILogger<StemsvModel> logger)
        {
            _logger = logger;
            _dbController = new DB_Controller();
        }

        public void OnGet()
        {
            DataTable groupData = _dbController.Exec_Queury("SELECT * FROM Student_groups");
            string groupHtml = "";
            var groupedData = groupData.AsEnumerable().GroupBy(row => row.Field<int>("group_no"));
            foreach (var group in groupedData)
            {
                int groupNo = group.Key;
                string groupName = $"Group {groupNo}";

                groupHtml += @"
                <div class='col-md-4'>
                  <div class='card'>
                    <div class='card-body'>
                      <h5 class='card-title'>Group: " + groupName + @"</h5>
                      <p class='card-text'>Group Members:</p>
                      <ul class='list-group'>";

                foreach (DataRow row in group)
                {
                    int studentId = Convert.ToInt32(row["Student_id"]);
                    groupHtml += "<li class='list-group-item'>Student ID: " + studentId + "</li>";
                }

                groupHtml += @"
                      </ul>
                    </div>
                  </div>
                </div>";
            }

            GroupsHtml = groupHtml;

            DataTable evalData = _dbController.Exec_Queury("SELECT * FROM trainer_eval");
            string trainerHtml = "";
            foreach (DataRow row in evalData.Rows)
            {
                int lectureId = Convert.ToInt32(row["lecture_id"]);
                int criteriaC1 = Convert.ToInt32(row["criteria_c1"]);
                int criteriaC2 = Convert.ToInt32(row["criteria_c2"]);
                int criteriaC3 = Convert.ToInt32(row["criteria_c3"]);
                int criteriaC4 = Convert.ToInt32(row["criteria_c4"]);
                DateTime date = Convert.ToDateTime(row["date"]);
                int attended = Convert.ToInt32(row["attended"]);

                string starRatingC1 = GetStarRating(criteriaC1);
                string starRatingC2 = GetStarRating(criteriaC2);
                string starRatingC3 = GetStarRating(criteriaC3);
                string starRatingC4 = GetStarRating(criteriaC4);

                trainerHtml += @"
<div class='col-md-4 mb-4 custom-card-margin '>
    <div class='card'>
        <img src='https://placehold.co/252' class='card-img-top' alt='...'>
        <div class='card-body'>
            <h5 class='card-title'>Trainer Evaluation - Lecture ID: " + lectureId + @"</h5>
            <p class='card-text'>Criteria:</p>
            <ul class='list-group'>
                <li class='list-group-item'>Criteria C1: " + starRatingC1 + @"</li>
                <li class='list-group-item'>Criteria C2: " + starRatingC2 + @"</li>
                <li class='list-group-item'>Criteria C3: " + starRatingC3 + @"</li>
                <li class='list-group-item'>Criteria C4: " + starRatingC4 + @"</li>
            </ul>
            <p class='card-text'>Date: " + date + @"</p>
            <p class='card-text'>Attended: " + attended + @"</p>
        </div>
    </div>
</div>";


            }

            TrainersHtml = trainerHtml;

        }

        private string GetStarRating(int rating)
        {
            string starRating = "";
            for (int i = 0; i < rating; i++)
            {
                starRating += "<i class='fas fa-star'></i>";
            }
            return starRating;
        }

        public IActionResult OnPost(int groupNumber, int studentId)
        {
            string insertQuery = $"INSERT INTO [dbo].[group] ([group_no]) VALUES ({groupNumber}); INSERT INTO [dbo].[Student_groups] ([group_no], [Student_id]) VALUES ({groupNumber}, {studentId})";
            _dbController.Exec_NonQ(insertQuery);

            return RedirectToPage("./Stemsv");
        }
        //public IActionResult OnPostAddTrainerEvaluation(int lectureId, int criteriaC1, int criteriaC2, int criteriaC3, int criteriaC4, DateTime date, int attended)
        //{

        //    _dbController.Exec_NonQ("INSERT INTO trainer_eval (lecture_id, criteria_c1, criteria_c2, criteria_c3, criteria_c4, date, attended) " +
        //        $"VALUES ({lectureId}, {criteriaC1}, {criteriaC2}, {criteriaC3}, {criteriaC4}, '{date.ToString("yyyy-MM-dd")}', {attended})");
        //    return RedirectToPage("/STEM_svpages/Stemsv");
        //}
        public IActionResult OnPostAddTrainerEvaluation(int lectureId, int criteriaC1, int criteriaC2, int criteriaC3, int criteriaC4, DateTime date, int attended)
        {
            try
            {
                // Check if the provided lecture ID exists in the lecture table
                bool isValidLectureId = CheckIfValidLectureId(lectureId);

                if (!isValidLectureId)
                {
                    throw new Exception("Invalid lecture ID. Please provide a valid lecture ID.");
                }

                // Insert the evaluation record
                _dbController.Exec_NonQ("INSERT INTO trainer_eval (lecture_id, criteria_c1, criteria_c2, criteria_c3, criteria_c4, date, attended) " +
                    $"VALUES ({lectureId}, {criteriaC1}, {criteriaC2}, {criteriaC3}, {criteriaC4}, '{date.ToString("yyyy-MM-dd")}', {attended})");

                return RedirectToPage("/STEM_svpages/Stemsv");
            }
            catch (Exception ex)
            {
                // Handle the exception and display an error message to the user
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private bool CheckIfValidLectureId(int lectureId)
        {
            string query = $"SELECT COUNT(*) FROM lecture WHERE lecture_id = {lectureId}";

            int? count = _dbController.Exec_Scalar(query);

            // If the count is greater than 0, the lecture ID exists
            return count > 0;
        }


    }
}
