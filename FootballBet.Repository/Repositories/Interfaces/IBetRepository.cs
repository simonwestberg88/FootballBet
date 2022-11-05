using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IBetRepository
{
    public Task<BetEntity> GetBetByIdAsync(int id);
    public Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBetsAsync(int matchId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, int matchId, string bettingGroupId);
    public Task<BetEntity> PlaceBetAsync(BetEntity bet);
    
}