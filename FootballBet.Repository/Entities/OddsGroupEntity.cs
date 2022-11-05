namespace FootballBet.Repository.Entities;

public class OddsGroupEntity
{
    public int Id { get; set; }
    public virtual ICollection<OddsEntity> OddsEntities { get; set; }
    public DateTime Created { get; set; }
}
