using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;
using CIE206PROJECT.Models.Chart;


using CIE206PROJECT.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Data : PageModel
    {
		public List<Tuple<int, string, string>> sortingList = new List<Tuple<int, string, string>>();
        [BindProperty]
        public string ChartJson { get; set; }
        public ChartJs Chart { get; set; }

		public ChartJs CriteriaChart { get; set; }
		public ChartJs StudentsChart { get; set; }
		public string criteriaChartJson { get; set; }

		public string studentsChartJson { get; set; }


		private readonly DB_Container _DBC;
		public DataPage _DB { get; set; }
		public DataTable dt { get; set; }
		public string _Handler { get; set; }
		public string data_of { get; set; }
		public string SortingBy { get; set; }

		private readonly ILogger<Data> _logger;


		public void initalizeSortingList()
		{ 
			int i = 0;
			string asc_dsc = string.Empty;
			foreach (DataColumn col in dt.Columns)
			{
				asc_dsc = "Ascending";

				sortingList.Add(new Tuple<int, string, string>(i, col.ColumnName, asc_dsc));
				i++;
				asc_dsc = "Descending";
				sortingList.Add(new Tuple<int, string, string>(i, col.ColumnName, asc_dsc));
				i++;
			}
		}

        public Data(ILogger<Data> logger, DB_Container container)
        {
            data_of= string.Empty;
            _logger = logger;
			SortingBy= "Sort by..";
			_DBC = container;
		}

        public void OnGet()
        {
			OnGetCourses();
		}
        public void OnGetCourses()
        {
			_DB = _DBC.dataPage_DB;
			_Handler = "Course";
			dt = _DB.getCourses();
			initalizeSortingList();
            data_of = "All Courses";
			init_chart_fin();

			//////////////////////////////

		}
		public void OnGetInstuctors()
        {
			_DB = _DBC.dataPage_DB;
			_Handler = "Inst";
			dt = _DB.getTrainerEval();
			initalizeSortingList();
			data_of = "Instructor Ratings";
			init_chart_tut();


		}
		public void OnGetStudents()
		{
			_DB = _DBC.dataPage_DB;
			_Handler = "Stu";
			dt = _DB.getStudentEval();
			initalizeSortingList();
			data_of = "Student Ratings";

		}
		public void OnGetFinances()
        {
			_DB = _DBC.dataPage_DB;
			dt = _DB.getFinances();
			_Handler = "Fin";
			data_of = "Course Finances";
			initalizeSortingList();
			init_chart_fin();

		}

		public void OnGetSort(string itemsort, string sortorder, string dataofin, string hndlr)
		{
			if (hndlr == "Fin")
			{
				_DB = _DBC.dataPage_DB;
				dt = _DB.sortFinances(itemsort, sortorder);
			}
			else if (hndlr == "Inst")
			{
				_DB = _DBC.dataPage_DB;
				dt = _DB.sortTrainerEval(itemsort, sortorder);
			}
			else if (hndlr == "Course")
			{
				_DB = _DBC.dataPage_DB;
				dt = _DB.sortCourses(itemsort, sortorder);

			}
			else if (hndlr == "Stu")
			{
				_DB = _DBC.dataPage_DB;
				dt = _DB.sortStudentEval(itemsort, sortorder);

			}

			data_of = dataofin;
			_Handler = hndlr;
			initalizeSortingList();
			SortingBy = $"{itemsort} {sortorder}";

		}


		private void init_chart_fin()
		{
			_DB = _DBC.dataPage_DB;
			DataTable datachartdt = _DB.getFinances();
			var chartData = @"
				{
				type: 'bar',
				responsive: true,
				data:
				{
				datasets: [{
				backgroundColor: [
				'rgba(255, 99, 132, 0.2)',
				'rgba(54, 162, 235, 0.2)',
				'rgba(157, 207, 170, 0.2)',
				'rgba(218, 137, 232, 0.2)',
				'rgba(160, 240, 156, 0.2)',
				'rgba(131, 27, 250, 0.2)',

				],
				borderColor: [
				'rgba(255, 99, 132, 1)',
				'rgba(54, 162, 235, 1)',
				'rgba(157, 207, 170,1)',
				'rgba(218, 137, 232,1)',
				'rgba(160, 240, 156,1)',
				'rgba(131, 27, 250, 1)',
				],
				borderWidth: 1
				}]
				},
				options:
				{
				scales:
				{
				y: [{
				ticks:
				{
				beginAtZero: true
				}
				}]
				}
				}
				}";
			Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
			ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
			{


				NullValueHandling = NullValueHandling.Ignore,
			});


			try
			{
				Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
				List<string> Labels = new List<string> { };
				List<double> Data = new List<double>();
				if (datachartdt is not null)
				{
					foreach (DataRow row in datachartdt.Rows)
					{
						int payment = (int)row["Payed (L.E)"];
						string Couse_Name = (string)row["Course Name"];

						int index = Labels.IndexOf(Couse_Name); // Find the index of the label in the list
						if (index == -1) // Check if the label is not in the list
						{
							Labels.Add(Couse_Name); // Add the label to the list
							Data.Add(payment); // Add the payment to the summedPayments list
						}
						else
						{
							Data[index] += payment; // Add the payment to the existing summed payment
						}
					}
				}

				Chart.data.datasets[0].label = "Courses Payment";
				Chart.data.datasets[0].data = Data.ToArray();
				Chart.data.labels = Labels.ToArray();

				ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				//throwing and exception caused a runtime error so this is a better way to go
				//around it until adding proper exception handling.
			}
		}

		private void init_chart_tut()
		{
			_DB = _DBC.dataPage_DB;
			DataTable datachartdt = _DB.getTrainerEval();
			var chartData = @"
				{
				type: 'bar',
				responsive: true,
				data:
				{
				datasets: [{
				backgroundColor: [
				'rgba(255, 99, 132, 0.2)',
				'rgba(54, 162, 235, 0.2)',
				'rgba(157, 207, 170, 0.2)',
				'rgba(218, 137, 232, 0.2)',
				'rgba(160, 240, 156, 0.2)',
				'rgba(131, 27, 250, 0.2)',

				],
				borderColor: [
				'rgba(255, 99, 132, 1)',
				'rgba(54, 162, 235, 1)',
				'rgba(157, 207, 170,1)',
				'rgba(218, 137, 232,1)',
				'rgba(160, 240, 156,1)',
				'rgba(131, 27, 250, 1)',
				],
				borderWidth: 1
				}]
				},
				options:
				{
				scales:
				{
				y: [{
				ticks:
				{
				beginAtZero: true
				}
				}]
				}
				}
				}";

			try
			{
				CriteriaChart = JsonConvert.DeserializeObject<ChartJs>(chartData);
				StudentsChart = JsonConvert.DeserializeObject<ChartJs>(chartData);
				// Initialize the lists for both charts
				List<string> criteriaLabels = new List<string>();
				List<double> criteriaAverages = new List<double>();
				List<int> criteriaCounts = new List<int>();

				List<string> studentsLabels = new List<string>();
				List<double> studentsAverages = new List<double>();
				List<int> studentsCounts = new List<int>();

				if (datachartdt is not null)
				{
					foreach (DataRow row in datachartdt.Rows)
					{
						string tutorName = (string)row["Tutor Name"];
						int criteria = (int)row["Average Criteria"];
						int students = (int)row["Number of Students"];

						// Update the lists for the criteria chart
						int criteriaIndex = criteriaLabels.IndexOf(tutorName);
						if (criteriaIndex == -1)
						{
							criteriaLabels.Add(tutorName);
							criteriaAverages.Add(criteria);
							criteriaCounts.Add(1);
						}
						else
						{
							criteriaAverages[criteriaIndex] += criteria;
							criteriaCounts[criteriaIndex]++;
						}

						// Update the lists for the students chart
						int studentsIndex = studentsLabels.IndexOf(tutorName);
						if (studentsIndex == -1)
						{
							studentsLabels.Add(tutorName);
							studentsAverages.Add(students);
							studentsCounts.Add(1);
						}
						else
						{
							studentsAverages[studentsIndex] += students;
							studentsCounts[studentsIndex]++;
						}
					}
				}

				// Calculate the average values for both charts
				for (int i = 0; i < criteriaAverages.Count; i++)
				{
					criteriaAverages[i] /= criteriaCounts[i];
				}
				for (int i = 0; i < studentsAverages.Count; i++)
				{
					studentsAverages[i] /= studentsCounts[i];
				}

				// Set the chart data for the criteria chart
				CriteriaChart.data.datasets[0].label = "Average Criteria per Tutor";
				CriteriaChart.data.datasets[0].data = criteriaAverages.ToArray();
				CriteriaChart.data.labels = criteriaLabels.ToArray();

				// Set the chart data for the students chart
				StudentsChart.data.datasets[0].label = "Average Number of Students per Tutor";
				StudentsChart.data.datasets[0].data = studentsAverages.ToArray();
				StudentsChart.data.labels = studentsLabels.ToArray();

				// Serialize the charts
				criteriaChartJson = JsonConvert.SerializeObject(CriteriaChart, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
				});
				studentsChartJson = JsonConvert.SerializeObject(StudentsChart, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}