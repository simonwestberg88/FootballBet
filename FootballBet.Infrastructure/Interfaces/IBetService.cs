namespace FootballBet.Infrastructure.Interfaces;

public interface IBetService
{
    public Task PlaceBetAsync(int oddsId, string userId, decimal amount, string bettingGroupId);
}