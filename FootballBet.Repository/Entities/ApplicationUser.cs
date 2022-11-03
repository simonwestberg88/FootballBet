using Microsoft.AspNetCore.Identity;

namespace FootballBet.Repository.Entities;

public class ApplicationUser : IdentityUser
{
    public decimal Balance { get; set; }
}