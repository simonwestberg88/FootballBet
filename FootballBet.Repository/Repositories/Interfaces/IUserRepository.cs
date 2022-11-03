using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetApplicationUserById(string userId, CancellationToken ct);
    public Task<decimal> UpdateBalance(string userId, decimal balance, CancellationToken ct);
}