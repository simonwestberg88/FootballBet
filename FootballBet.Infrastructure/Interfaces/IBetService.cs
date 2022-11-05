using FootballBet.Repository.Entities;

namespace FootballBet.Infrastructure.Interfaces;

public interface IBetService
{
    public Task PlaceBetAsync(int oddsId, string userId, decimal amount, string groupId);
    public Task<IEnumerable<BetEntity>> GetBetsForUserAsync(string userId, string groupId);
}