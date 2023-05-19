using System.Data;

namespace CIE206PROJECT.Controllers
{
	public class DataPage
	{
		private DB_Controller _Controller;
		public DataTable? getFinances() 
		{
			string q = "select transaction_no as [Transaction Number], course.course_id as [Course ID], one_two_time as [One/Two Payements], amount_payed as [Payed (L.E)] from (course_payment join [group] on group_id = group_no) join (offering join course on course.course_id = offering.course_id) on offering.offering_id = [group].offering_id";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? sortFinances(string by, string order) 
		{
			if (order == "ascending") order = "asc";
			else order = "desc";
			string q = $"select transaction_no as [Transaction Number], course.course_id as [Course ID], one_two_time as [One/Two Payements], amount_payed as [Payed (L.E)] from (course_payment join [group] on group_id = group_no) join (offering join course on course.course_id = offering.course_id) on offering.offering_id = [group].offering_id order by {by} {order}";
			DataTable? dt = new DataTable();
			dt = _Controller.Exec_Queury(q);
			return dt;
		}


	}
}
