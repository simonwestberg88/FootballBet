using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Data.Settings;
using FootballBet.Server.Models.Football.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.Leagues;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace FootballBet.Server.Data.Services.APIs
{
    public class FootballApi : IFootballApi
    {
        private readonly FootballApiSettings _footballApiSettings;
        private const string ENDPOINT_LEAGUES = "leagues";
        private const string ENDPOINT_FIXTURES = "fixtures";

        public FootballApi(IOptions<FootballApiSettings> footballApiSettings)
            => _footballApiSettings = footballApiSettings.Value;

        #region LeaguesAndCompetitions
        public async Task<Models.Football.Leagues.Root> GetSpecificLeague(string id = "1")
        {
            var client = GetClient("/" + ENDPOINT_LEAGUES);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddParameter("id", id);
            var response = await client.GetAsync(request);

            var deserializedResponse = JsonConvert.DeserializeObject<Models.Football.Leagues.Root>(response.Content);

            return deserializedResponse;
        }
        #endregion
        #region Fixtures
        public async Task<List<Match>> GetFixtures(int leagueId, string season)
        {
            var client = GetClient(ENDPOINT_FIXTURES);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddParameter("league", leagueId);
            request.AddParameter("season", season);
            var response = await client.GetAsync(request);
            try
            {
                var deserializedResponse = JsonConvert.DeserializeObject<Models.Football.ApiResponses.Fixtures.Root>(response.Content);
                return deserializedResponse.Matches;
            }
            catch (Exception)
            {

                return new List<Match>();
            }


        }
        #endregion
        private RestClient GetClient(string urlPart)
        {
            var client = new RestClient($"{_footballApiSettings.Url}{_footballApiSettings.Version}/{urlPart}");
            client.AddDefaultHeader("x-rapidapi-key", _footballApiSettings.Key);
            client.AddDefaultHeader("x-rapidapi-host", _footballApiSettings.Host);

            return client;

        }
    }
}
