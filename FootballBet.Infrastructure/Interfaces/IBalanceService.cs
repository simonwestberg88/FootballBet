namespace FootballBet.Infrastructure.Interfaces;

public interface IBalanceService
{
    public Task<decimal> GetBalance(string userId, CancellationToken cancellationToken = default);
    public Task<decimal> WithdrawAsync(string userId, decimal amount, CancellationToken cancellationToken = default);
    public Task<decimal> DepositAsync(string userId, decimal amount, CancellationToken cancellationToken = default);
}