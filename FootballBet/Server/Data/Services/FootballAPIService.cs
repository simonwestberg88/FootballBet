using FootballBet.Server.Data.Repositories;
using FootballBet.Server.Data.Services.Interfaces;
using RestSharp;

namespace FootballBet.Server.Data.Services
{
    public class FootballAPIService : IFootballAPIService
    {
        private readonly IFootballApi _footballAPI; 
        public FootballAPIService(IFootballApi footballApi)
            => _footballAPI = footballApi;

        public async Task<string> GetLeagues()
        {
            var result = await _footballAPI.GetAllLeagues();
            return "ok";
        }
    }
}
