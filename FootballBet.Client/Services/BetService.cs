using System.Net.Http.Json;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Client.Services;

public interface IBetService
{
    Task<BetResponse?> GetBetAsync(int matchId, string groupId);
    public Task<BetResponse> PlaceBetAsync(int oddsId, int matchId, decimal amount, string groupId);
}

public class BetService : IBetService
{
    private readonly HttpClient _httpClient;

    public BetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BetResponse?> GetBetAsync(int matchId, string groupId)
    {
        var response =  await _httpClient.GetAsync($"/api/bets/match?matchId={matchId}&groupId={groupId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BetResponse>();
        }
        return null;
    }

    public async Task<BetResponse> PlaceBetAsync(int oddsId, int matchId, decimal amount, string groupId)
    {
        var bet = new BetRequest
        {
            OddsId = oddsId,
            Amount = amount,
            GroupId = groupId,
            MatchId = matchId
        };
        var response = await _httpClient.PostAsJsonAsync($"api/bets/place", bet);
        return await response.Content.ReadFromJsonAsync<BetResponse>();
    }
}