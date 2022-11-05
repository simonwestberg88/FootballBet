using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FootballBet.Repository.Entities;

public class MatchOddsEntity
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    [ForeignKey("MatchId")]
    public virtual Match Match { get; set; }
    public DateTime Created { get; set; }
    public virtual ICollection<OddsEntity> OddsEntities {get; set;}
}