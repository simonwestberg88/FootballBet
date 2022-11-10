using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class BaseOddsEntity
{
    public int Id { get; set; }
    public decimal Odds { get; set; }
    public MatchWinnerEntityEnum MatchWinnerEntityEnum { get; set; }
    
    public int MatchOddsGroupId { get; set; }
}