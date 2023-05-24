using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int user_id { get; set; }

        [Key]
        [Required]
        public int parent_id { get; set; }

        [Key]
        [Required]
        public string skill_level { get; set; }
    }
}
