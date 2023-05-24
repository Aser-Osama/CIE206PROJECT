
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Content
    {
        [Key]
        [Required]
        public int content_id { get; set; }

        [Key]
        [Required]
        public int course_id { get; set; }

        [Key]
        [Required]
        public string summary { get; set; }

        [Key]
        [Required]
        public string summary_vid { get; set; }

        [Key]
        [Required]
        public string slides { get; set; }

        [Key]
        [Required]
        public string teacher_guide { get; set; }

        [Key]
        [Required]
        public string handout { get; set; }



    }
}
