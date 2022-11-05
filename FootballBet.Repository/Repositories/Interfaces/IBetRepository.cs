using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IBetRepository
{
    public Task<BetEntity> GetBetByIdAsync(int id);
    public Task<IEnumerable<BetEntity>> GetBetsByUserIdAsync(string userId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBetsByMatchIdAsync(int matchId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBets(string userId, int matchId, string bettingGroupId);
    public Task<BetEntity> PlaceBetAsync(BetEntity bet);
    
}