using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IBetRepository
{
    public Task<BetEntity> GetBetByIdAsync(int id);
    public Task<IEnumerable<BetEntity>> GetBetsByUserIdAsync(string userId);
    public Task<IEnumerable<BetEntity>> GetBetsByMatchIdAsync(int matchId);
    public Task<IEnumerable<BetEntity>> GetBetsByUserIdAndMatchIdAsync(int userId, int matchId);
    public Task<IEnumerable<BetEntity>> GetAllBetsAsync();
    public Task DeleteBetAsync(int id);
}