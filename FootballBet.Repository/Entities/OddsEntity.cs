using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class OddsEntity
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    [ForeignKey("MatchId")]
    public virtual Match Match { get; set; }
    public string ExactResult { get; set; }
    public Prediction Prediction { get; set; }
    public DateTime OddsDate { get; set; }
}