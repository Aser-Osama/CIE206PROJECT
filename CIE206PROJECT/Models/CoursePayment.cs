using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class CoursePayment
    {
        [Key]
        [Required]
        public int parent_id { get; set; }

        [Key]
        [Required]
        public int group_id { get; set; }

        [Key]
        [Required]
        public int transaction_no { get; set; }

        [Key]
        [Required]
        public string one_two_time { get; set; }

        [Key]
        [Required]
        public string v_cash_msg { get; set; }

        [Key]
        [Required]
        public int amount_payed { get; set; }

    }
}
