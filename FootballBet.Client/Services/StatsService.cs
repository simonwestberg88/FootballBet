using System.Net.Http.Json;
using FootballBet.Shared.Models.Stats;

namespace FootballBet.Client.Services;

public interface IStatsService
{
    public Task<BetStatsDto> GetAppBarStats(string groupId);
    public Task<WinStatsResponse> GetTop10WinStats(string groupId);
    public Task<ChartResponse> GetChartStats(string groupId);

}
public class StatsService: IStatsService
{
    private readonly HttpClient _httpClient;
    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<BetStatsDto> GetAppBarStats(string groupId)
    {
        var response = await _httpClient.GetFromJsonAsync<BetStatsDto>($"api/stats/appbar?groupId={groupId}");
        return response;
    }
    
    public async Task <WinStatsResponse> GetTop10WinStats(string groupId)
    {
        var response = await _httpClient.GetFromJsonAsync<WinStatsResponse>($"api/stats/topwins?groupId={groupId}");
        return response;
    }
    
    public async Task<ChartResponse> GetChartStats(string groupId)
    {
        var response = await _httpClient.GetFromJsonAsync<ChartResponse>($"api/stats/chart?groupId={groupId}");
        return response;
    }
}