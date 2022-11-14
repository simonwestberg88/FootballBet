using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using Match = FootballBet.Infrastructure.ApiResponses.Fixtures.Match;

namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballApiClient
{
    public Task<LeaguesRoot> GetSpecificLeague(string id);
    public Task<List<Match>> GetFixtures(int leagueId, string season);
    public Task<IEnumerable<OddsResponse>> GetOddsAsync(int leagueId, string season);
    public Task<OddsResponse?> GetOddsAsync(int matchId);
}