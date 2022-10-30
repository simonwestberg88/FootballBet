using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;

namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballApiClient
{
    public Task<LeaguesRoot> GetSpecificLeague(string id);
    Task<List<Match>> GetFixtures(int leagueId, string season);
}