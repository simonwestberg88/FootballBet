using FootballBet.Server.Models.Football.ApiResponses.Leagues;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IFootballApiClient
    {
        public Task<LeaguesRoot> GetSpecificLeague(string id);
        Task<List<Models.Football.ApiResponses.Fixtures.Match>> GetFixtures(int leagueId, string season);
    }
}
