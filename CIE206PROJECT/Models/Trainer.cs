using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Trainer
    {
        [Key]
        [Required]
        public int user_id { get; set; }

        [Key]
        [Required]
        public string level { get; set; }

        [Key]
        [Required]
        public string field { get; set; }



    }
}
