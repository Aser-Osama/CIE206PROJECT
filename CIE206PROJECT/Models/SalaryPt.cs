using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class SalaryPt
    {
        [Key]
        [Required]
        public int user_id { get; set; }

        [Key]
        [Required]
        public int hours_worked { get; set; }

        [Key]
        [Required]
        public int pay_per_session { get; set; }

        [Key]
        [Required]
        public int pay_in_month { get; set; }


    }
}
