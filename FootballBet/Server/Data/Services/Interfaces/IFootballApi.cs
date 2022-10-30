using FootballBet.Server.Models.Football.Leagues;
using RestSharp;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IFootballApi
    {
        public Task<Root> GetSpecificLeague(string id);
        Task<List<Models.Football.ApiResponses.Fixtures.Match>> GetFixtures(int leagueId, string season);
    }
}
