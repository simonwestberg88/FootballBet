using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddsAsync(IEnumerable<OddsEntity> oddsList);
    public Task<IEnumerable<OddsEntity>> GetOddsForMatchAsync(int matchId);
    
}