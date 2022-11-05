using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Repository.Entities;

public class BetEntity
{
    public int Id { get; set; }
    public int? MatchId { get; set; }
    [ForeignKey("MatchId")]
    public virtual MatchEntity Match { get; set; }
    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; }
    public Guid? BettingGroupId { get; set; }
    [ForeignKey("BettingGroupId")]
    public virtual BettingGroupEntity? BettingGroup { get; set; }
    public int? OddsId { get; set; }
    [ForeignKey("OddsId")]
    public virtual OddsEntity OddsEntity { get; set; }
    public decimal WagerAmount { get; set; }
    public decimal PaybackAmount { get; set; }
    public bool? IsWinningBet { get; set; }
    public bool HasBeenPayed { get; set; }
}