using System.Data;

namespace CIE206PROJECT.Controllers
{
	public class DataPage
	{
		private DB_Controller _Controller;

		public DataPage()
		{
			_Controller = new DB_Controller();
		}

		public DataTable? getFinances()
		{
			string q = "SELECT cp.transaction_no AS [Transaction Number], c.course_id AS [Course ID], c.course_name AS [Course Name], cp.one_two_time AS [One/Two Payments], cp.amount_payed AS [Payed (L.E)] " +
			"FROM course_payment AS cp " +
			"JOIN [group] AS g ON cp.group_id = g.group_no " +
			"JOIN offering AS o ON g.offering_id = o.offering_id " +
			"JOIN course AS c ON o.course_id = c.course_id; ";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? sortFinances(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";

			string q = "SELECT cp.transaction_no AS [Transaction Number], c.course_id AS [Course ID], c.course_name AS [Course Name], cp.one_two_time AS [One/Two Payments], cp.amount_payed AS [Payed (L.E)] " +
						"FROM course_payment AS cp " +
						"JOIN [group] AS g ON cp.group_id = g.group_no " +
						"JOIN offering AS o ON g.offering_id = o.offering_id " +
						"JOIN course AS c ON o.course_id = c.course_id " +
						$"ORDER BY [{by}] {order};";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? getCourses()
		{
			string q = "SELECT c.course_id AS [Course ID], c.video_link AS [Video Link],  c.course_name AS [Course Name], LEFT(c.advertisement_text, 100) AS [Ad. Text] " +
						"FROM course AS c " +
						"JOIN offering AS o ON c.course_id = o.course_id " +
						"JOIN [group] AS g ON g.offering_id = o.offering_id " +
						"GROUP BY c.course_id, c.video_link, c.advertisement_text, c.course_name;";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}
		public DataTable? sortCourses(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = "SELECT c.course_id AS [Course ID], c.video_link AS [Video Link],  c.course_name AS [Course Name], SUM(g.n_students) AS [Student Count], LEFT(c.advertisement_text, 100) AS [Ad. Text] " +
			"FROM course AS c " +
			"JOIN offering AS o ON c.course_id = o.course_id " +
			"JOIN [group] AS g ON g.offering_id = o.offering_id " +
			"GROUP BY c.course_id, c.video_link, c.advertisement_text" +
			$"ORDER BY [{by}] {order};";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? getTrainerEval()
		{

			string q= @"
					SELECT
						te.lecture_id AS [Lecture ID],
						te.[date] AS [Lecture Date],
						c.course_id AS [Course ID],
						c.course_name AS [Course Name],
						t.[name] AS [Tutor Name],
						(te.criteria_c1 + te.criteria_c2 + te.criteria_c3 + te.criteria_c4) / 4 AS [Average Criteria],
						COUNT(se.student_id) AS [Number of Students]
					FROM
						trainer_eval AS te
						JOIN lecture AS l ON te.lecture_id = l.lecture_id
						JOIN content AS ct ON l.content_id = ct.content_id
						JOIN course AS c ON ct.course_id = c.course_id
						JOIN [group] AS g ON l.group_id = g.group_no
						JOIN [user] AS t ON g.Trainer_id = t.[user_id]
						JOIN student_eval AS se ON se.lecture_id = l.lecture_id
					GROUP BY
						te.lecture_id,
						te.[date],
						c.course_id,
						c.course_name,
						t.[name],
						(te.criteria_c1 + te.criteria_c2 + te.criteria_c3 + te.criteria_c4) / 4";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}
		public DataTable? sortTrainerEval(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = $@"
							SELECT
								te.lecture_id AS [Lecture ID],
								te.[date] AS [Lecture Date],
								c.course_id AS [Course ID],
								c.course_name AS [Course Name],
								t.[name] AS [Tutor Name],
								(te.criteria_c1 + te.criteria_c2 + te.criteria_c3 + te.criteria_c4) / 4 AS [Average Criteria],
								COUNT(se.student_id) AS [Number of Students]
							FROM
								trainer_eval AS te
								JOIN lecture AS l ON te.lecture_id = l.lecture_id
								JOIN content AS ct ON l.content_id = ct.content_id
								JOIN course AS c ON ct.course_id = c.course_id
								JOIN [group] AS g ON l.group_id = g.group_no
								JOIN [user] AS t ON g.Trainer_id = t.[user_id]
								JOIN student_eval AS se ON se.lecture_id = l.lecture_id
							GROUP BY
								te.lecture_id,
								te.[date],
								c.course_id,
								c.course_name,
								t.[name],
								(te.criteria_c1 + te.criteria_c2 + te.criteria_c3 + te.criteria_c4) / 4
							ORDER BY
								[{by}] {order};";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}

		public DataTable? getStudentEval()
		{

			string q = $@"
            SELECT
                l.lecture_id AS [Lecture ID],
                l.[day] AS [Lecture Date],
                c.course_id AS [Course ID],
                c.course_name AS [Course Name],
                t.[name] AS [Tutor Name],
                s.[name] AS [Student Name],
                (se.criteria_c1 + se.criteria_c2 + se.criteria_c3 + se.criteria_c4) / 4 AS [Average Student Rating]
            FROM
                lecture AS l
                JOIN content AS ct ON l.content_id = ct.content_id
                JOIN course AS c ON ct.course_id = c.course_id
                JOIN [group] AS g ON l.group_id = g.group_no
                JOIN [user] AS t ON g.Trainer_id = t.[user_id]
                JOIN student_eval AS se ON se.lecture_id = l.lecture_id
                JOIN [user] AS s ON se.student_id = s.[user_id];";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}
		public DataTable? sortStudentEval(string by, string order)
		{
			if (order == "Ascending") order = "asc";
			else order = "desc";
			string q = $@"
            SELECT
                l.lecture_id AS [Lecture ID],
                l.[day] AS [Lecture Date],
                c.course_id AS [Course ID],
                c.course_name AS [Course Name],
                t.[name] AS [Tutor Name],
                s.[name] AS [Student Name],
                (se.criteria_c1 + se.criteria_c2 + se.criteria_c3 + se.criteria_c4) / 4 AS [Average Student Rating]
            FROM
                lecture AS l
                JOIN content AS ct ON l.content_id = ct.content_id
                JOIN course AS c ON ct.course_id = c.course_id
                JOIN [group] AS g ON l.group_id = g.group_no
                JOIN [user] AS t ON g.Trainer_id = t.[user_id]
                JOIN student_eval AS se ON se.lecture_id = l.lecture_id
                JOIN [user] AS s ON se.student_id = s.[user_id]
            ORDER BY
                [{by}] {order};";
 
			DataTable? dt = _Controller.Exec_Queury(q);
			return dt;
		}
	}
}
