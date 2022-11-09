using FootballBet.Repository.Entities;

namespace FootballBet.Infrastructure.Interfaces;

public interface IBetService
{
    public Task<BetEntity> PlaceBetAsync(int oddsId, string userId, decimal amount, string groupId);
    public Task<IEnumerable<BetEntity>> GetBets(string userId, string groupId);
    public Task<BetEntity> GetBet(string userId, int matchId, string groupId);

}