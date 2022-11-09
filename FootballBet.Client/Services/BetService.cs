using System.Net.Http.Json;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Client.Services;
public interface IBetService
{
    Task<IEnumerable<BetDto>> GetBetsAsync(string groupId);
    Task<BetDto> PlaceBetAsync(int oddsId, decimal amount, string groupId);
}
public class BetService : IBetService
{
    private readonly HttpClient _httpClient;
    public BetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public Task<IEnumerable<BetDto>> GetBetsAsync(string groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<BetDto> PlaceBetAsync(int oddsId, decimal amount, string groupId)
    {
        var bet = new BetDto
        {
            OddsId = oddsId,
            Amount = amount
        };
        var response = await _httpClient.PostAsJsonAsync($"api/bets/place?groupId={groupId}", bet);
        return await response.Content.ReadFromJsonAsync<BetDto>();
    }
}