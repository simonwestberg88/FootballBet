using System.ComponentModel.DataAnnotations.Schema;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class OddsEntity
{
    public int Id { get; set; }
    public int MatchOddsId { get; set; }
    [ForeignKey("MatchOddsId")]
    public virtual MatchOddsEntity MatchOdds { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
    public decimal Odds { get; set; }
    public DateTime Created { get; set; }
    public OddsType OddsType { get; set; }
}