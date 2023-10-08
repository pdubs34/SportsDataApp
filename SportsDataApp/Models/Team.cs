namespace SportsDataApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Abbreviation { get; set; }

        public int SportId { get; set; }
        public Sport? Sport { get; set; }

    }
}
