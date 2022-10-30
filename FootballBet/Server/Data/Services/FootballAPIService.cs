using FootballBet.Server.Data.Mappers;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Models.Football.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.DBModels;

namespace FootballBet.Server.Data.Services
{
    public class FootballAPIService : IFootballAPIService
    {
        private readonly IFootballApiClient _footballApiClient;
        private readonly IFootballRepository _footballRepository;

        public FootballAPIService(IFootballApiClient footballApiClient, IFootballRepository footballRepository)
        {
            _footballApiClient = footballApiClient;
            _footballRepository = footballRepository;
        }

        public async Task<string> GetWorldCup()
        {
            var result = await _footballApiClient.GetSpecificLeague("1");
            var resultTwo = await _footballApiClient.GetFixtures(1, "2018");
            var test = _footballRepository.GetAllMatchesForLeagueId(1);
            return "ok";
        }

        public async Task<string> SeedDatabase()
        {
            var matches = (await _footballApiClient.GetFixtures(1, "2018"));
            var league = await _footballRepository.CreateOrUpdateLeague(matches.First().League.ToLeagueEntity());
            var teams = new List<Team>();
            teams.AddRange(matches.Select(m => m.Teams.Home).ToList());
            teams.AddRange(matches.Select(m => m.Teams.Away).ToList());
            teams = teams.DistinctBy(t => t.Id).ToList();
            var result = await _footballRepository.CreateOrUpdateTeams(teams.Select(t => t.ToTeamEntity()));
            var fixtures = await _footballRepository.CreateOrUpdateMatches(matches.Select(m => m.ToMatchEntity()));

            
            return "hello";

        }       
    }
}
