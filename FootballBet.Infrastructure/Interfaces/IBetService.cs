using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Interfaces;

public interface IBetService
{
    public Task<BetEntity> PlaceBetAsync(int oddsId, int matchId, string userId, decimal amount, string groupId);
    public Task<IEnumerable<BetRequest>> GetBets(string userId, string groupId);
    public Task<BetResponse?> GetBet(string userId, int matchId, string groupId);
    public Task<List<GroupVisibleBetDto>> GetBetsForGameAsync(int matchId, string groupId);

}