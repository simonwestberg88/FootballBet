using FootballBet.Server.Models.Football.Leagues;
using RestSharp;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IFootballApi
    {
        public Task<List<Response>> GetAllLeagues();
    }
}
