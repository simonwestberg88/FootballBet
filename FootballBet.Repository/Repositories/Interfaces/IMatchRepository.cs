using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IMatchRepository
{
    public Task<IEnumerable<MatchEntity>> GetMatches(int leagueId);
    public Task<MatchEntity?> GetMatchAsync(int matchId);
}