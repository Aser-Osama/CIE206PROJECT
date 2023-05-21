using System.Data;

namespace CIE206PROJECT.Controllers
{
    public class CoursePage
    {
        private DB_Controller _Controller;

        public CoursePage()
        {
            _Controller = new DB_Controller();
        }

        public DataTable? getGroupsTrainer(int id)
        {
            string q = $@"
                    SELECT g.group_no, c.course_name
                    FROM [group] g
                    JOIN offering o ON g.offering_id = o.offering_id
                    JOIN course c ON o.course_id = c.course_id
                    WHERE g.Trainer_id = {id};
            ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getGroupsStudent(int id)
        {
            string q = $@"
                       SELECT [group].group_no, course.course_name, [user].[name] AS tutor_name
                    FROM Student_groups
                    JOIN [group] ON Student_groups.group_no = [group].group_no
                    JOIN offering ON [group].offering_id = offering.offering_id
                    JOIN course ON offering.course_id = course.course_id
                    JOIN Trainer ON [group].Trainer_id = Trainer.user_id
                    JOIN [user] ON Trainer.user_id = [user].user_id
                    WHERE Student_groups.Student_id = {id};
               ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }





        public DataTable? getUserInfo(int id)
        {
            string q = $@"
                        SELECT [user].user_id,
                               [user].[name],
                               [user].email,
                               [user].[address],
                               [user].join_date,
                               [user].date_of_birth,
                               [user].user_type
                        FROM [user]
                        WHERE [user].user_id = {id};";

            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }



        public DataTable? getStudentInfo(int id)
        {
            string q = $@"
                        SELECT
                            Student.skill_level,
                            [parent_user].[name] AS parent_name,
                            [user].[name] AS student_name
                        FROM Student
                        JOIN [user] ON Student.user_id = [user].user_id
                        LEFT JOIN [user] AS [parent_user] ON Student.parent_id = [parent_user].user_id
                        WHERE Student.user_id = {id};
                    ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getStudentId(int id)
        {
            string q = $@"
                    SELECT Student.user_id
                    FROM Student
                    JOIN [user] AS ParentUser ON Student.parent_id = ParentUser.user_id
                    WHERE ParentUser.user_id = {id};
                   ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }




        public DataTable? getUserPhonenumbers(int id)
        {
            string q = $@"
                SELECT phone_num
                FROM [phone_num] 
                WHERE user_id={id};
                ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }


        public DataTable? getTrainerInfo(int id)
        {
            string q = $@"
                    SELECT
                    Trainer.[level],
                    Trainer.field
                    FROM Trainer
                    WHERE Trainer.user_id = {id};
                "; 
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }
        public DataTable? getTrainerEvaluations(int id)
        {
            string q = $@"
                        SELECT g.group_no, 
                               AVG(te.criteria_c1) AS avg_criteria_c1, 
                               AVG(te.criteria_c2) AS avg_criteria_c2, 
                               AVG(te.criteria_c3) AS avg_criteria_c3, 
                               AVG(te.criteria_c4) AS avg_criteria_c4
                        FROM [group] g
                        JOIN lecture l ON g.group_no = l.group_id
                        JOIN trainer_eval te ON l.lecture_id = te.lecture_id
                        WHERE g.Trainer_id = {id}
                        GROUP BY g.group_no;
                        ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getStudentEvaluations(int id)
        {
			string q = $@"
            SELECT g.group_no,
                AVG(se.criteria_c1) AS avg_criteria_c1,
                AVG(se.criteria_c2) AS avg_criteria_c2,
                AVG(se.criteria_c3) AS avg_criteria_c3,
                AVG(se.criteria_c4) AS avg_criteria_c4
            FROM [group] g
            JOIN lecture l ON g.group_no = l.group_id
            LEFT JOIN student_eval se ON l.lecture_id = se.lecture_id
            WHERE se.student_id = {id}
            GROUP BY g.group_no;
            ";

			DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }
        public DataTable? getStudentAttendance(int id)
        {
			string q = $@"
            SELECT g.group_no,
                SUM(CAST(se.attendance AS float)) / COUNT(se.attendance) AS avg_attendance
            FROM [group] g
            JOIN lecture l ON g.group_no = l.group_id
            LEFT JOIN student_eval se ON l.lecture_id = se.lecture_id
            WHERE se.student_id = {id}
            GROUP BY g.group_no;
            ";

			DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }


        public DataTable? getGroupContent(int id)
        {
            string q = $@"
                    SELECT
                      content.content_id,
                      content.summary,
                      content.summary_vid,
                      content.slides,
                      content.teacher_guide,
                      content.handout
                    FROM content
                    JOIN course ON content.course_id = course.course_id
                    JOIN offering ON course.course_id = offering.course_id
                    JOIN [group] ON offering.offering_id = [group].offering_id
                    WHERE [group].group_no ={id}
					ORDER BY content.content_id ASC;
            ";
            Console.WriteLine(q);
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getGroupContentTopics(int id)
        {
            string q = $@"
                SELECT [topic], [topic_description]
                FROM [content_topics]
                WHERE [content_id] = {id};
           ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public int getStudentCount(int id){
            string q = $@"
                        SELECT COUNT(Student_groups.Student_id) AS num_students
                        FROM Student_groups
                        WHERE Student_groups.group_no ={id};
                        ";
            int dt = (int)_Controller.Exec_Scalar(q);
            return dt;
 
        }

        public DataTable? getGroupInfo(int id)
        {
            string q = $@"
                SELECT
                  course.course_name,
                  course.course_description,
                  course.tot_sessions,
                  offering.Start_Date,
                  offering.Price,
                  [group].group_no,
                  [group].Trainer_id,
                  [group].Timeslot,
                  [group].meeting_link,
                  [group].age_grp,
                  [user].email AS tutor_email
                FROM [group]
                JOIN offering ON [group].offering_id = offering.offering_id
                JOIN course ON offering.course_id = course.course_id
                LEFT JOIN Trainer ON [group].Trainer_id = Trainer.user_id
                LEFT JOIN [user] ON Trainer.user_id = [user].user_id
                WHERE [group].group_no = {id};
           ";

            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getStudentsGroup(int id)
        {
            string q = $@"
                    SELECT [user].user_id,[user].[name] AS student_name, [user].profile_pic
                    FROM Student_groups
                    JOIN [user] ON Student_groups.Student_id = [user].user_id
                    WHERE Student_groups.group_no = {id};
          ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }

        public DataTable? getStudentsNotes(int id)
        {
            string q =$@"
                SELECT r.subject, r.content, r.datetime AS date_sent, u.name AS sender_name, u.user_type AS sender_user_type
                FROM request r
                JOIN [user] u ON r.sent_by = u.user_id
                WHERE r.sent_to = {id};
          ";

            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }


        public bool createStudentNote()
        {
            string q = "";
            bool dt = _Controller.Exec_NonQ(q);

            return dt;
        }
        public bool deleteStudentNote(int id)
        {
            string q = $@"
                    DELETE FROM request
                    WHERE sent_to = {id};
                    ";
            bool dt = _Controller.Exec_NonQ(q);
            return dt;
        }

        public DataTable? getUpcomingLecture(int id)
        {
            string q = $@"SELECT TOP 1
                          lecture.lecture_id,
                          lecture.day,
                          lecture.room
                        FROM lecture
                        JOIN [group] ON lecture.group_id = [group].group_no
                        WHERE [group].group_no = 19
                        ORDER BY lecture.day DESC;

                        ";
            DataTable? dt = new DataTable();
            dt = _Controller.Exec_Queury(q);
            return dt;
        }
    }
}
