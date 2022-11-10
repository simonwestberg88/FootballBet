using System.Net.Http.Json;
using FootballBet.Shared.Models.Odds;

namespace FootballBet.Client.Services;

public interface IOddsService
{
    public Task<IEnumerable<ExactScoreOddsDto>?> GetExactScoreOdds(int matchId);
    public Task<BaseOddsResponse> GetBaseOdds(int matchId);
}

public class OddsService : IOddsService
{
    private readonly HttpClient _httpClient;

    public OddsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ExactScoreOddsDto>?> GetExactScoreOdds(int matchId)
        => await _httpClient.GetFromJsonAsync<IEnumerable<ExactScoreOddsDto>>($"api/matches/odds?matchId={matchId}");

    public async Task<BaseOddsResponse> GetBaseOdds(int matchId)
        => await _httpClient.GetFromJsonAsync<BaseOddsResponse>($"api/matches/baseOdds?matchId={matchId}");
}