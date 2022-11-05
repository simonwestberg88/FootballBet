using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Repository.Entities;

public class BetEntity
{
    public int Id { get; set; }
    public int? MatchId { get; set; }
    public string? UserId { get; set; }
    public Guid? BettingGroupId { get; set; }
    public int? OddsId { get; set; }
    public decimal WagerAmount { get; set; }
    public decimal PaybackAmount { get; set; }
    public bool? IsWinningBet { get; set; }
    public bool HasBeenPayed { get; set; }
}