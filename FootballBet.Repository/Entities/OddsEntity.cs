using System.ComponentModel.DataAnnotations.Schema;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class OddsEntity
{
    public int Id { get; set; }
    public int HomeTeamGoals { get; set; }
    public int AwayTeamGoals { get; set; }
    public decimal Odds { get; set; }
    public MatchWinnerEntityEnum MatchWinnerEntityEnum { get; set; }
    
    public int? MatchOddsGroupId { get; set; }
    [ForeignKey("MatchOddsGroupId")]
    public virtual MatchOddsGroupEntity MatchOddsGroupEntity { get; set; }
}