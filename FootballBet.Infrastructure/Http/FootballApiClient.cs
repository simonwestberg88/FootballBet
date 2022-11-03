using System.Net.Http.Json;
using System.Text.Json;
using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using Microsoft.Extensions.Options;
using Match = FootballBet.Infrastructure.ApiResponses.Fixtures.Match;

namespace FootballBet.Infrastructure.Http;

public class FootballApiClient : IFootballApiClient
{
    private readonly HttpClient _client;
    private const int Bet365 = 8;

    public FootballApiClient(IOptions<FootballApiSettings> options, HttpClient client)
    {
        _client = client;
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
            var response = await _client.GetFromJsonAsync<OddsRoot>($"/v3/odds?league={leagueId}&season={season}&bookmaker={Bet365}&date={DateTime.Now:yyyy-MM-dd}");
        }
        catch (Exception e)
        {
            return;
        }
    }

    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content);
        return result;
    }
}