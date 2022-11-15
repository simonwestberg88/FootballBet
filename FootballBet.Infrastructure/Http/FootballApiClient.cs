using System.Net.Http.Json;
using System.Text.Json;
using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Infrastructure.Constants;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Match = FootballBet.Infrastructure.ApiResponses.Fixtures.Match;

namespace FootballBet.Infrastructure.Http;

public class FootballApiClient : IFootballApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<FootballApiClient> _logger;

    public FootballApiClient(
        IOptions<FootballApiSettings> options,
        HttpClient client, 
        ILogger<FootballApiClient> logger)
    {
        _client = client;
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

    public async Task<Match?> GetMatch(int matchId)
    {
        var fixture = await _client.GetFromJsonAsync<FixturesRoot>($"/v3/fixtures?id={matchId}");
        var match = fixture?.Matches.FirstOrDefault();
        return match ?? new Match();
    }

    public async Task<IEnumerable<OddsResponse>> GetOddsAsync(int leagueId, string season)
    {
        var response = await _client.GetFromJsonAsync<OddsRoot>($"/v3/odds?league={leagueId}&season={season}&bookmaker={ApiConstants.Bet365Id}");
        return response.Response;
    }

    public async Task<OddsResponse?> GetOddsAsync(int matchId)
    {
        var response = await _client.GetFromJsonAsync<OddsRoot>($"/v3/odds?fixture={matchId}&bookmaker={ApiConstants.Bet365Id}");
        return response.Response.FirstOrDefault();
    }


    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content);
        return result;
    }
}