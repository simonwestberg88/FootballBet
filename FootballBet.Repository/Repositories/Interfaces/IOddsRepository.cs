using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddsAsync(IEnumerable<OddsEntity> oddsEntities);
    public Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup);
    public Task<IEnumerable<OddsEntity>> GetOddsByMatchIdAsync(int matchId);
}