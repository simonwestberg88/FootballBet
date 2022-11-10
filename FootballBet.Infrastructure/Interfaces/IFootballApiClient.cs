using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using FootballBet.Shared.Models.Odds;

namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballApiClient
{
    public Task<LeaguesRoot> GetSpecificLeague(string id);
    public Task<List<Match>> GetFixtures(int leagueId, string season);
    public Task SaveOddsForLeague(int leagueId, string season);
    public Task<IEnumerable<ExactScoreOddsDto>> GetLatestExactScoreOdds(int matchId);
    public Task<BaseOddsResponse> GetLatestBaseOdds(int matchId);
}