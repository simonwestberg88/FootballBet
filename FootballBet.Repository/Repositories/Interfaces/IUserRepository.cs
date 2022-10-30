using FootballBet.Repository.Entities;
using FootballBet.Server.Models;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser> GetApplicationUserById(string userId, CancellationToken ct);
}