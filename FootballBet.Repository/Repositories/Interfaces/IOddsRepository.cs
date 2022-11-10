using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddsAsync(IEnumerable<ExactScoreOddsEntity> oddsEntities);
    public Task AddBaseOddsAsync(IEnumerable<BaseOddsEntity> baseOddsEntities);
    public Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup);
    public Task<IEnumerable<ExactScoreOddsEntity>> GetLatestExactScoreOddsAsync(int matchId);
    public Task<IEnumerable<BaseOddsEntity>> GetLatestBaseOddsAsync(int matchId);
    public Task<ExactScoreOddsEntity?> GetOddsAsync(int exactOddsId);
    public Task<BaseOddsEntity?> GetBaseOddsAsync(int exactOddsId, MatchWinnerEntityEnum winner);
}