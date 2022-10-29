using System.ComponentModel.DataAnnotations;

namespace FootballBet.Server.Models.Football.DBModels
{
    public class TeamEntity
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }

    }
}
