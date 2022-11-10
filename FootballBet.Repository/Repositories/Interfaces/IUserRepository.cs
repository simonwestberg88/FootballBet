using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetUserAsync(string userId, CancellationToken ct);
    public Task<decimal> GetBalanceAsync(string userId, string groupId, CancellationToken ct);
}