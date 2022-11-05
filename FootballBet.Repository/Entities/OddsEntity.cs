using System.ComponentModel.DataAnnotations.Schema;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class OddsEntity
{
    public int Id { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
    public decimal Odds { get; set; }
    public OddsType OddsType { get; set; }
    
    public int? MatchOddsGroupId { get; set; }
    [ForeignKey("MatchOddsGroupId")]
    public virtual MatchOddsGroupEntity MatchOddsGroupEntity { get; set; }
}