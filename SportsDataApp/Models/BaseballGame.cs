namespace SportsDataApp.Models
{
    public class BaseballGame
    {
        public int Id { get; set; }
        public int RunsScored { get; set; }
        public int RunsAllowed { get; set; }
        public string? WorL {  get; set; }

        public int SeasonId { get; set; }  // This property represents the foreign key

        public Season? Season { get; set; }

    }
}
