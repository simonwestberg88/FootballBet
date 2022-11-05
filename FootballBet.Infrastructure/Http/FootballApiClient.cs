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
using Microsoft.Extensions.Options;
using Match = FootballBet.Infrastructure.ApiResponses.Fixtures.Match;

namespace FootballBet.Infrastructure.Http;

public class FootballApiClient : IFootballApiClient
{
    private readonly HttpClient _client;
    private readonly IFootballRepository _footballRepository;
    private readonly IOddsRepository _oddsRepository;
    private const int Bet365 = 8;

    public FootballApiClient(
        IOptions<FootballApiSettings> options, 
        HttpClient client, 
        IFootballRepository footballRepository,
        IOddsRepository oddsRepository)
    {
        _client = client;
        _footballRepository = footballRepository;
        _oddsRepository = oddsRepository;
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

    public async Task GetOddsForLeague(int leagueId, string season)
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
                
                var oddsEntities = CreateOddsEntities(bookmakerOddsForMatch, matchId);
                await _oddsRepository.AddOddsAsync(oddsEntities);
                
            }
            //add odds to db and tie them to groups and matches
            await _oddsRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return;
        }
    }

    private static IEnumerable<OddsEntity> CreateOddsEntities(BookmakerOdds bookmakerOdds, int matchId)
    {
        var betValues = bookmakerOdds.Bets
            .SingleOrDefault(b => b.Name is "Match Winner" or "Exact Score").Values;
        return betValues.Select(w => w.ToOddsEntity(matchId)).ToList();
    }

    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content);
        return result;
    }
}