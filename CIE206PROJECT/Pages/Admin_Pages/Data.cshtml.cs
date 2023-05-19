using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Data : PageModel
    {
		public List<Tuple<int, string, string>> sortingList = new List<Tuple<int, string, string>>();



		public DataTable dt { get; set; }
		public string _Handler { get; set; }
		public string data_of { get; set; }
		public string SortingBy { get; set; }

		private readonly ILogger<Data> _logger;

		public void initalizeSortingList()
		{
			dt = new DataTable();
			dt.Columns.Add("SNO", typeof(int));
			dt.Columns.Add("Name", typeof(string));
			dt.Columns.Add("City", typeof(string));
			dt.Columns.Add("Date", typeof(DateTime));
			dt.Rows.Add(1, "Siva", "TUP", DateTime.Now);
			dt.Rows.Add(2, "Raman", "MAS", DateTime.Now);
			dt.Rows.Add(3, "Sivaraman", "TRY", DateTime.Now);
			dt.Rows.Add(4, "Kuble", "MDU", DateTime.Now);
			dt.Rows.Add(5, "Arun", "Salem", DateTime.Now);
			dt.Rows.Add(6, "Kumar", "Erode", DateTime.Now);
			dt.Rows.Add(7, "ghasj", "Tup", DateTime.Now);
			dt.Rows.Add(8, "dsfd", "yercaud", DateTime.Now);
			dt.Rows.Add(9, "dsdf", "ui", DateTime.Now);

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

        public Data(ILogger<Data> logger)
        {
            data_of= string.Empty;
            _logger = logger;
			SortingBy= "Sort by..";

		}

        public void OnGet()
        {
			
			Console.WriteLine("general");
            OnGetCourses();


		}
        public void OnGetCourses()
        {
			initalizeSortingList();

            data_of = "All Courses";


		}
        public void OnGetInstuctors()
        {
			initalizeSortingList();
			data_of = "Instructor Ratings and Comments";


		}
		public void OnGetFinances()
        {
			initalizeSortingList();
			data_of = "Course Finances";


		}
		public void OnGetSort(int id, string dataofin)
		{

			
			initalizeSortingList();
			var t = sortingList[id];
			data_of = dataofin;
			SortingBy = $"{t.Item2} {t.Item3}";
			Console.WriteLine($"SORTINGGGGGGGGGGGGGGGGGG = {t.Item1} {t.Item2} {t.Item3}");
			
			

		}
	}
}