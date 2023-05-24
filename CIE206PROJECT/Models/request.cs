using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
    public class Request
    {

        public int request_id { get; set; }

        public string content { get; set; }


        public string subject { get; set; }


        public string datetime { get; set; }

        public int sent_by { get; set; }


        public int sent_to { get; set; }


    }
}
