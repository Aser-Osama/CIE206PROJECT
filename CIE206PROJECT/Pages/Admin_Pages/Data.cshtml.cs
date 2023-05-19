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

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Data : PageModel
    {
		public List<Tuple<int, string, string>> sortingList = new List<Tuple<int, string, string>>();


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
		}
        public void OnGetInstuctors()
        {
			_DB = _DBC.dataPage_DB;
			_Handler = "Inst";
			dt = _DB.getTrainerEval();
			initalizeSortingList();
			data_of = "Instructor Ratings and Comments";

		}
		public void OnGetFinances()
        {
			_DB = _DBC.dataPage_DB;
			dt = _DB.getFinances();
			_Handler = "Fin";
			data_of = "Course Finances";
			initalizeSortingList();
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

			data_of = dataofin;
			_Handler = hndlr;
			initalizeSortingList();
			SortingBy = $"{itemsort} {sortorder}";

		}
	}
}