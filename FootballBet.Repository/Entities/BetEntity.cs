using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Entities;

public class BetEntity
{
    public int MatchId { get; set; }
    public virtual MatchEntity? Match { get; set; }

    public int Id { get; set; }
    public string? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    public Guid BettingGroupId { get; set; }
    
    public virtual BettingGroupEntity? BettingGroup { get; set; }
    public Prediction Prediction { get; set; }
    public decimal WagerAmount { get; set; }
    public decimal PaybackAmount { get; set; }
    public bool IsWinningBet { get; set; }
    public bool HasBeenPayed { get; set; }
}