namespace FootballBet.Repository.Entities;

public class MatchOddsGroupEntity
{
    public int Id { get; set; }
    public int? MatchId { get; set; }
    public DateTime Created { get; set; }
}
