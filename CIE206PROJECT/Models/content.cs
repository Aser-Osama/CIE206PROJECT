namespace CIE206PROJECT.Models
{
    public class Content
    {
        public int content_id { get; set; }
        public int course_id { get; set; }
        public string summary { get; set; }
        public string summary_vid { get; set; }
        public string slides { get; set; }
        public string teacher_guide { get; set; }
        public string handout { get; set; }
    }
}