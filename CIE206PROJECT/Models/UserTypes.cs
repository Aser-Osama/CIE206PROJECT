using CIE206PROJECT.Pages;

namespace CIE206PROJECT.Models
{
    public class UserTypes
    {
        public enum Logged_In_Type
        {
            Student,
            Parent,
            Trainer,
            Sales,
            Op_mngr,
            CEO,
            Coordinator,
            Content_Dev,
            Supervisor,
            Senior_supervisor
        }

        public string TypeToString(Logged_In_Type type)
        {
            string userTypeString = type.ToString();
            return userTypeString;
        }

        public Logged_In_Type? StringToType(string type)
        {

            Logged_In_Type userType;
            if (Enum.TryParse(type, out userType))
            {
                return userType;
            }
            else
            {
                return null;
            }

        } 
    }
}
