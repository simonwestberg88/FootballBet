using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IMatchRepository
{
    public Task<IEnumerable<MatchEntity>> GetMatches(int leagueId);
    public Task<MatchEntity?> GetMatchAsync(int matchId);
    public Task<IEnumerable<MatchEntity>> GetUnprocessedMatchesAsync();
    public Task SetProcessedAsync(int matchId);
    public Task<IEnumerable<MatchEntity>> GetFinishedMatches(int leagueId = 1, int? season = 2022);
}