using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Repository.Entities
{
    public class LeagueEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public int? Season { get; set; }
        public ICollection<MatchEntity> Matches { get; set; }
        public virtual ICollection<BettingGroupEntity> BettingGroups { get; set; }
    }
}
