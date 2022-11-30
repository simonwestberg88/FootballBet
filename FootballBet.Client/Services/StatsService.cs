using System.Net.Http.Json;
using FootballBet.Shared.Models.Stats;

namespace FootballBet.Client.Services;

public interface IStatsService
{
    public Task<AppBarStatsDto> GetAppBarStats(string groupId);
}
public class StatsService: IStatsService
{
    private readonly HttpClient _httpClient;
    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<AppBarStatsDto> GetAppBarStats(string groupId)
    {
        var response = await _httpClient.GetFromJsonAsync<AppBarStatsDto>($"api/stats/appbar?groupId={groupId}");
        return response;
    }
}