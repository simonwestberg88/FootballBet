using System.Net.Http.Json;

namespace FootballBet.Client.Services;
public interface IBalanceService
{
    Task<decimal> GetBalanceAsync(string userId, CancellationToken token);
    Task<decimal> DepositAsync(string userId, decimal amount);
    Task<decimal> WithdrawAsync(string userId, decimal amount);
}
public class BalanceService: IBalanceService
{
    private readonly HttpClient _httpClient;
    public BalanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<decimal> GetBalanceAsync(string userId, CancellationToken token)
    {
        var response = await _httpClient.GetAsync($"api/balance/{userId}", token);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<decimal>();
    }
    public async Task<decimal> DepositAsync(string userId, decimal amount)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/balance/{userId}/deposit", amount);
        response.EnsureSuccessStatusCode();
        var balance = await response.Content.ReadFromJsonAsync<decimal>();
        return balance;
    }
    public async Task<decimal> WithdrawAsync(string userId, decimal amount)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/balance{userId}/withdraw", amount);
        response.EnsureSuccessStatusCode();
        var balance = await response.Content.ReadFromJsonAsync<decimal>();
        return balance;
    }
}