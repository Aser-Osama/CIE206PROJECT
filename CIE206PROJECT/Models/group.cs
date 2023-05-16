namespace CIE206PROJECT.Models
{
    public class Group
    {
        public int group_no { get; set; }
        public int offering_id { get; set; }
        public int Trainer_id { get; set; }
        public object Timeslot { get; set; }
        public int n_students { get; set; }
        public string meeting_link { get; set; }
        public string age_grp { get; set; }
    }
}