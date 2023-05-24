using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class PhoneNum
    {
        [Key]
        [Required]
        public int user_id { get; set; }

        [Key]
        [Required]
        public string phone_num { get; set; }


    }
}
