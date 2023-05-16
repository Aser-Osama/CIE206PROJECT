namespace CIE206PROJECT.Models
{
    public class Lecture
    {
        public int lecture_id { get; set; }
        public int content_id { get; set; }
        public int group_id { get; set; }
        public object day { get; set; }
        public string room { get; set; }
    }
}