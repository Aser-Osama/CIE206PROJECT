using System.Data;
using CIE206PROJECT.Models;

namespace CIE206PROJECT.Controllers
{
    public class RequestsPage
    {
        private DB_Controller _Controller;

        public RequestsPage()
        {
            _Controller = new DB_Controller();
        }

        public int getMaxReqID()
        {
            string q = "select max(request_id) from request";
            int? id = _Controller.Exec_Scalar(q);
            return id ?? -1;
        }

        public int getCEOID()
        {
            string q = "select top 1 [USER_ID] from [user] where user_type = 'CEO'";
            int? id = _Controller.Exec_Scalar(q);
            return id ?? -1;

        }
        public int getSSID()
        {
            string q = "select top 1 [USER_ID] from [user] where user_type = 'SENIOR_SUPERVISOR'";
            int? id = _Controller.Exec_Scalar(q);
            return id ?? -1;

        }

        public bool addRequest(Request r)
        {
            string q = $@"
            insert into request
            values 
            ({r.request_id}, '{r.content}' , '{r.subject}', '{r.datetime}', {r.sent_by}, {r.sent_to})
            ";
            return _Controller.Exec_NonQ(q);
        }

        public DataTable getRequests(int pid){
            string q = $@"select request_id as [Req. ID], r.[subject] as [Subject], r.[datetime] as [Time],
                        u.[name] as [Sent By], u.user_type as [Sender Dept.]
                        from request as r join [user] as u on u.[user_id] =  r.sent_by
                        where r.sent_to  = {pid};";
            return _Controller.Exec_Queury(q) ?? new DataTable();
        }

        public string getRequests(int pid, int rid)
        {
            string q = $@"
                select request.content from request
                where request.sent_to = {pid} and request_id = {rid};";
            
            return (string)_Controller.Exec_Queury(q).Rows[0][0] ?? string.Empty;
        }
    }
}
