using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IBetRepository
{
    public Task<BetEntity> GetBetByIdAsync(int id);
    public Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBetsAsync(int matchId, string bettingGroupId);
    public Task<IEnumerable<BetEntity>> GetBetsForGroupAsync(string bettingGroupId);
    public Task<BetEntity?> GetBetAsync(string userId, int matchId, string groupId);
    public Task<List<BetEntity>> GetBetsForGroupAndGameAsync(int matchId, string groupId);
    public Task<BetEntity> PlaceBetAsync(BetEntity bet);
    public Task<IEnumerable<BetEntity>> GetUnprocessedBetsAsync(int matchIds);
    public Task ProcessExactWinAsync(int betId);
    public Task ProcessBaseWinAsync(int betId);
    public Task ProcessLossAsync(int betId);
    public Task<UserBalanceEntity> GetUserBalanceForGroupAsync(string userId, string groupId);
    public Task<IEnumerable<UserBalanceEntity>> GetUserBalancesForGroupAsync(string groupId);
    public Task<StatEntity> GetBetStatsAsync(string groupId, string userId);
    public Task<IEnumerable<StatEntity>> GetBetStatsAsync(string groupId);

}