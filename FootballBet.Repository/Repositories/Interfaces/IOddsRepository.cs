using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddsAsync(IEnumerable<ExactScoreOddsEntity> oddsEntities);
    public Task AddBaseOddsAsync(IEnumerable<BaseOddsEntity> baseOddsEntities);
    public Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup);
    public Task<IEnumerable<ExactScoreOddsEntity>> GetLatestExactScoreOddsAsync(int matchId);
    public Task<BaseOddsEntity> GetLatestBaseOddsAsync(int matchId);
    public Task<ExactScoreOddsEntity?> GetOddsAsync(int oddsId);
    public Task<ExactScoreOddsEntity?> GetBaseOddsAsync(int oddsId, MatchWinnerEntityEnum winner);
}