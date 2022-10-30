using System.Text.Json;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Models.Football.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.ApiResponses.Leagues;

namespace FootballBet.Server.Data.Services.APIs;

public class FootballApiClientClient : IFootballApiClient
{
    private readonly HttpClient _client;

    public FootballApiClientClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<LeaguesRoot> GetSpecificLeague(string id = "1")
    {
        var response = await _client.GetAsync($"/leagues?id={id}");
        var leaguesRoot = await HandleResponse<LeaguesRoot>(response);
        return leaguesRoot ?? new LeaguesRoot();
    }
    public async Task<List<Match>> GetFixtures(int leagueId, string season)
    {
        var response = await _client.GetAsync($"/fixtures?league={leagueId}&season={season}");
        try
        {
            var root = await HandleResponse<Root>(response);
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