using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetApplicationUserById(string userId, CancellationToken ct);
    public Task<decimal> GetBalanceAsync(string userId, CancellationToken ct);
    public Task<decimal> WithdrawAsync(string userId, decimal amount, CancellationToken cancellationToken = default);
    public Task<decimal> DepositAsync(string userId, decimal amount, CancellationToken cancellationToken = default);
}