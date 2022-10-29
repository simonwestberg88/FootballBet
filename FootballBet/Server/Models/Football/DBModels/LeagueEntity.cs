using System.ComponentModel.DataAnnotations;

namespace FootballBet.Server.Models.Football.DBModels
{
    public class LeagueEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public int? Season { get; set; }
        public ICollection<MatchEntity> Matches { get; set; }
    }
}
