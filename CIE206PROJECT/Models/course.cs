using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int course_id { get; set; }

        [Key]
        [Required]
        public string course_name { get; set; }

        [Key]
        [Required]
        public string course_description { get; set; }

        [Key]
        [Required]
        public int tot_sessions { get; set; }

        [Key]
        [Required]
        public string advertisement_text { get; set; }

        [Key]
        [Required]
        public string video_link { get; set; }

    }
}
