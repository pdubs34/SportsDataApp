namespace SportsDataApp.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int year { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public double winPercent { get; set; }
        public string? outcome { get; set; }
        public int TeamId { get; set; }
        public Team? Team { get; set; }

    }
}
