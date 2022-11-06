using System.Net.Http.Json;
using FootballBet.Shared.Models.Odds;

namespace FootballBet.Client.Services;

public interface IOddsService
{
    public Task<IEnumerable<OddsDto>?> GetOdds(int matchId);
}

public class OddsService : IOddsService
{
    private readonly HttpClient _httpClient;

    public OddsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OddsDto>?> GetOdds(int matchId)
        => await _httpClient.GetFromJsonAsync<IEnumerable<OddsDto>>($"api/matches/odds?matchId={matchId}");
}