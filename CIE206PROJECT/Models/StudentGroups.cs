using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class StudentGroups
    {
        [Key]
        [Required]
        public int group_no { get; set; }

        [Key]
        [Required]
        public int Student_id { get; set; }

    }
}
