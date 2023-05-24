using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Offering
    {
        [Key]
        [Required]
        public int offering_id { get; set; }

        [Key]
        [Required]
        public int course_id { get; set; }

        [Key]
        [Required]
        public DateTime Start_Date { get; set; }

        [Key]
        [Required]
        public int Price { get; set; }

    }
}
