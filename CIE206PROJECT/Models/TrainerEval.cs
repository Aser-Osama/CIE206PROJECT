using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class TrainerEval
    {
        [Key]
        [Required]
        public int lecture_id { get; set; }

        [Key]
        [Required]
        public int criteria_c1 { get; set; }

        [Key]
        [Required]
        public int criteria_c2 { get; set; }

        [Key]
        [Required]
        public int criteria_c3 { get; set; }

        [Key]
        [Required]
        public int criteria_c4 { get; set; }

        [Key]
        [Required]
        public DateTime date { get; set; }

        [Key]
        [Required]
        public int attended { get; set; }

    }
}
