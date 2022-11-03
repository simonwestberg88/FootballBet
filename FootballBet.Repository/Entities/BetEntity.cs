using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class BetEntity
{
    public virtual MatchEntity? Match { get; set; }
    public int Id { get; set; }
    public virtual ApplicationUser? User { get; set; }
    public BetWager Wager { get; set; }
    public decimal WagerAmount { get; set; }
    public decimal PaybackAmount { get; set; }
    public bool IsWinningBet { get; set; }
    public bool HasBeenPayed { get; set; }
}