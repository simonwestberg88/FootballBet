using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddsAsync(IEnumerable<OddsEntity> oddsEntities);
    public Task AddBaseOddsAsync(IEnumerable<BaseOddsEntity> baseOddsEntities);
    public Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup);
    public Task<IEnumerable<OddsEntity>> GetLatestOddsAsync(int matchId);
    public Task<OddsEntity?> GetOddsAsync(int oddsId);
    public Task<OddsEntity?> GetBaseOddsAsync(int oddsId, MatchWinnerEntityEnum winner);
}