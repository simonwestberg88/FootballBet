using Microsoft.AspNetCore.Identity;

namespace FootballBet.Repository.Entities;

public class ApplicationUser : IdentityUser
{
    public decimal Balance { get; set; }
    public virtual ICollection<BetEntity> Bets { get; set; }
}