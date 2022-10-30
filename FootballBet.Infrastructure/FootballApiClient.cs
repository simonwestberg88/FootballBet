using System.Text.Json;
using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;
using Microsoft.Extensions.Options;

namespace FootballBet.Infrastructure;

public class FootballApiClient : IFootballApiClient
{
    private readonly HttpClient _client;
    public FootballApiClient(IOptions<FootballApiSettings> options,  HttpClient client)
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

    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content);
        return result;
    }
}