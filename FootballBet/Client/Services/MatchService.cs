using FootballBet.Shared.Models.Match;
using System.Net.Http.Json;

namespace FootballBet.Client.Services;

public interface IMatchService
{
    Task<IEnumerable<MatchDto>> GetMatchesAsync();
}
public class MatchService: IMatchService
{
    private readonly HttpClient _httpClient;
    public MatchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesAsync()
    {
        Console.WriteLine($"Sending to {_httpClient.BaseAddress}");
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<MatchDto>>("/api/matches/1");
        return result;
    }
}