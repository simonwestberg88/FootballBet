using Microsoft.AspNetCore.Identity;

namespace FootballBet.Repository.Entities;

public class ApplicationUser : IdentityUser
{
    public double Balance { get; set; }
}