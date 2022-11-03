namespace FootballBet.Infrastructure.Interfaces;

public interface IBalanceService
{
    public Task<double> GetBalance(string userId, CancellationToken cancellationToken = default);
    public Task<double> UpdateBalance(string userId, double amount, CancellationToken cancellationToken = default);
}