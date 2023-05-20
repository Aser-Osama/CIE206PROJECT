using Microsoft.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;

namespace CIE206PROJECT.Controllers
{
    public class DB_Controller
    {
        public string Connection_string = "Data Source=LAPTOP-EOAIN8PD;Initial Catalog=THIS_IS_THE_DATABASE;Integrated Security=True";

        public DataTable? Exec_Queury(string q) //returns null if there was an error
        {
            using (var Connection =new SqlConnection(Connection_string))
            {
                using (var command = new SqlCommand(q, Connection))
                {
                    try
                    {
                        Connection.Open();
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        Connection.Close();
                        return dt;

                    }
                    catch (SqlException) 
                    {
                        Connection.Close();
                        return null;
                    }
                }
            }
        }

        public int? Exec_Scalar(string q) //returns null if there was an error
        {
            using (var connection = new SqlConnection(Connection_string))
            {
                using (var command = new SqlCommand(q, connection))
                {
                    try
                    {
                        connection.Open();
                        int scalar = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();
                        return scalar;
                    }
                    catch (SqlException)
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
        }





        public bool Exec_NonQ(string q) //returns false if there was an error
        {
            using (var connection = new SqlConnection(Connection_string))
            {
                using (var command = new SqlCommand(q, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                    catch (SqlException)
                    {
                        connection.Close();
                        return false;
                    }
                }
            }
        }

        public DataTable ColumnNames(string TableName) 
        {
            string q = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{TableName}'";
            return Exec_Queury(q);
        }

    }
}
