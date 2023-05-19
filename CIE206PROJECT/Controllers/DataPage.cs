using System.Data;

namespace CIE206PROJECT.Controllers
{
	public class DataPage
	{
		private DB_Controller _Controller;

		public DataPage()
		{
			_Controller= new DB_Controller();
		}

		public DataTable? getFinances() 
		{
			string q = "select transaction_no as [Transaction Number], course.course_id as [Course ID], one_two_time as [One/Two Payements], amount_payed as [Payed (L.E)] from (course_payment join [group] on group_id = group_no) join (offering join course on course.course_id = offering.course_id) on offering.offering_id = [group].offering_id";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? sortFinances(string by, string order) 
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = $"select transaction_no as [Transaction Number], course.course_id as [Course ID], one_two_time as [One/Two Payements], amount_payed as [Payed (L.E)] from (course_payment join [group] on group_id = group_no) join (offering join course on course.course_id = offering.course_id) on offering.offering_id = [group].offering_id order by [{by}] {order}";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? getCourses()
		{
			string q = "select course.course_id as [Course ID], Course.Video_Link as [Video Link], sum([group].n_students) as [Student Count],  LEFT(course.advertisement_text, 50) AS [Ad. Text] from (course join offering on course.course_id = offering.course_id) join [group] on [group].offering_id = offering.offering_id group by course.course_id, Course.Video_Link, course.advertisement_text";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}
		public DataTable? sortCourses(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = $"select course.course_id as [Course ID], Course.Video_Link as [Video Link], sum([group].n_students) as [Student Count],  LEFT(course.advertisement_text, 50) AS [Ad. Text] from (course join offering on course.course_id = offering.course_id) join [group] on [group].offering_id = offering.offering_id group by course.course_id, Course.Video_Link, course.advertisement_text order by [{by}] {order}";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? getTrainerEval ()
		{
			string q = "select [user].[name] as [Trainer Name], course.course_id as [Course ID], [trainer_eval].[date] as [Lecture Date], (criteria_c1 + criteria_c2 + criteria_c3 + criteria_c4)/4 as [Avg Eval/10], trainer_eval.attended as [N of Students] from ((((trainer_eval join lecture on trainer_eval.lecture_id = lecture.lecture_id) join [group] on [group].group_no = lecture.group_id) join [user] on [group].Trainer_id = [user].[user_id]) join content on content.content_id = lecture.content_id) join course on course.course_id = content.course_id";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}
		public DataTable? sortTrainerEval(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = $"select [user].[name] as [Trainer Name], course.course_id as [Course ID], [trainer_eval].[date] as [Lecture Date], (criteria_c1 + criteria_c2 + criteria_c3 + criteria_c4)/4 as [Avg Eval/10], trainer_eval.attended as [N of Students] from ((((trainer_eval join lecture on trainer_eval.lecture_id = lecture.lecture_id) join [group] on [group].group_no = lecture.group_id) join [user] on [group].Trainer_id = [user].[user_id]) join content on content.content_id = lecture.content_id) join course on course.course_id = content.course_id order by [{by}] {order}";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}
	}
}
