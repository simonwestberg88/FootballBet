using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Data.Settings;
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

        public FootballApi(IOptions<FootballApiSettings> footballApiSettings)
            => _footballApiSettings = footballApiSettings.Value;

        #region LeaguesAndCompetitions
        public async Task<List<Response>> GetAllLeagues()
        {
            //var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3/fixtures/rounds?league=333&season=2022");
            var client = new RestClient(_footballApiSettings.Url + _footballApiSettings.Version + @"/" + ENDPOINT_LEAGUES + "?id=1");
            //var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3/leagues");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("x-rapidapi-key", _footballApiSettings.Key);
            request.AddHeader("x-rapidapi-host", _footballApiSettings.Host);
            var response = await client.ExecuteAsync(request);
            if (response.Content == null)
                return null;
            var content = response.Content;
            try
            {
                var deserializedResponse = JsonConvert.DeserializeObject<Root>(response.Content);

            }
            catch (Exception)
            {

                throw;
            }

            return new List<Response>();
        }
        #endregion
        #region Fixtures

        #endregion
    }
}
