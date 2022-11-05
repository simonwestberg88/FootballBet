using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IMatchRepository
{
    public Task<IEnumerable<MatchEntity>> GetMatchesForLeague(int leagueId);
    public Task<MatchEntity?> GetMatch(int matchId);
}