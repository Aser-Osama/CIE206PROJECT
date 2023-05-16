namespace CIE206PROJECT.Models
{
    public class Course_payment
    {
        public int parent_id { get; set; }
        public int group_id { get; set; }
        public int transaction_no { get; set; }
        public string one_two_time { get; set; }
        public string v_cash_msg { get; set; }
        public int amount_payed { get; set; }
    }
}