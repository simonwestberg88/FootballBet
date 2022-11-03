using System.Net.Http.Json;

namespace FootballBet.Client.Services;
public interface IBalanceService
{
    Task<decimal> GetBalanceAsync();
    Task<decimal> DepositAsync(decimal amount);
    Task<decimal> WithdrawAsync(decimal amount);
}
public class BalanceService: IBalanceService
{
    private readonly HttpClient _httpClient;
    public BalanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<decimal> GetBalanceAsync()
    {
        var response = await _httpClient.GetAsync("api/balance");
        response.EnsureSuccessStatusCode();
        var balance = await response.Content.ReadFromJsonAsync<decimal>();
        return balance;
    }
    public async Task<decimal> DepositAsync(decimal amount)
    {
        var response = await _httpClient.PostAsJsonAsync("api/balance/deposit", amount);
        response.EnsureSuccessStatusCode();
        var balance = await response.Content.ReadFromJsonAsync<decimal>();
        return balance;
    }
    public async Task<decimal> WithdrawAsync(decimal amount)
    {
        var response = await _httpClient.PostAsJsonAsync("api/balance/withdraw", amount);
        response.EnsureSuccessStatusCode();
        var balance = await response.Content.ReadFromJsonAsync<decimal>();
        return balance;
    }
}