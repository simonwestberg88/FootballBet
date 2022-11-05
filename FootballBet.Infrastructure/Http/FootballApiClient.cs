using System.Net.Http.Json;
using System.Text.Json;
using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Infrastructure.Settings;
using FootballBet.Infrastructure.TestData;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Match = FootballBet.Infrastructure.ApiResponses.Fixtures.Match;

namespace FootballBet.Infrastructure.Http;

public class FootballApiClient : IFootballApiClient
{
    private readonly HttpClient _client;
    private readonly IFootballRepository _footballRepository;
    private readonly IOddsRepository _oddsRepository;
    private readonly ILogger<FootballApiClient> _logger;
    private const int Bet365 = 8;

    public FootballApiClient(
        IOptions<FootballApiSettings> options, 
        HttpClient client, 
        IFootballRepository footballRepository,
        IOddsRepository oddsRepository, ILogger<FootballApiClient> logger)
    {
        _client = client;
        _footballRepository = footballRepository;
        _oddsRepository = oddsRepository;
        _logger = logger;
        client.BaseAddress = new Uri(options.Value.Url);
        client.DefaultRequestHeaders.Add("x-rapidapi-key", options.Value.Key);
        client.DefaultRequestHeaders.Add("x-rapidapi-host", options.Value.Host);
    }

    public async Task<LeaguesRoot> GetSpecificLeague(string id = "1")
    {
        var response = await _client.GetAsync($"v3/leagues?id={id}");
        var leaguesRoot = await HandleResponse<LeaguesRoot>(response);
        return leaguesRoot ?? new LeaguesRoot();
    }

    public async Task<List<Match>> GetFixtures(int leagueId, string season)
    {
        var response = await _client.GetAsync($"/v3/fixtures?league={leagueId}&season={season}");
        try
        {
            var root = await HandleResponse<FixturesRoot>(response);
            return root?.Matches ?? new List<Match>();
        }
        catch (Exception)
        {
            return new List<Match>();
        }
    }

    public async Task SaveOddsForLeague(int leagueId, string season)
    {
        try
        {
            var matchIds = _footballRepository.GetAllMatchesForLeagueId(1).Select(m => m.Id).ToList();
            // var response = await _client.GetFromJsonAsync<OddsRoot>($"/v3/odds?league={leagueId}&season={season}&bookmaker={Bet365}&date={DateTime.Now:yyyy-MM-dd}");
            var matches = OddsTestData.GenerateData(matchIds).Response.ToList();
            //add MatchOddsEntity for all matches
            foreach (var matchId in matchIds)
            {
                var bookmakerOddsForMatch = matches
                    .SingleOrDefault(m => m.Match.Id == matchId)
                    .Bookmakers.SingleOrDefault(b => b.Id == Bet365);

                var oddsGroupId = await _oddsRepository.AddOddsGroupAsync(new MatchOddsGroupEntity{MatchId = matchId, Created = DateTime.Now});
                
                var oddsEntities = CreateOddsEntities(bookmakerOddsForMatch, matchId, oddsGroupId);
                await _oddsRepository.AddOddsAsync(oddsEntities);
                _logger.LogInformation("Saved odds for match {MatchId}", matchId);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving odds for league");
        }
    }

    private static IEnumerable<OddsEntity> CreateOddsEntities(BookmakerOdds bookmakerOdds, int matchId, int matchOddsGroupId)
    {
        var betValues = bookmakerOdds.Bets
            .Where(b => b.Name is "Match Winner" or "Exact Score").SelectMany(b => b.Values).ToList();
        return betValues.Select(w => w.ToOddsEntity(matchId, matchOddsGroupId)).ToList();
    }

    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content);
        return result;
    }
}