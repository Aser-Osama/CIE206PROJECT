using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class SalaryFt
    {
        [Key]
        [Required]
        public int OT_TIME_OFF { get; set; }

        [Key]
        [Required]
        public int monthly { get; set; }



    }
}
