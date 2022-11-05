namespace FootballBet.Repository.Entities;

public class MatchOddsGroupEntity
{
    public int Id { get; set; }
    public int? MatchId { get; set; }
    public virtual ICollection<OddsEntity> OddsEntities { get; set; }
    public DateTime Created { get; set; }
}
