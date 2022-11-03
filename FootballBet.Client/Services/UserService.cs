using System.Net.Http.Json;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Client.Services;

public interface IUserService
{
    public Task<UserDto> GetUserAsync(string userId);
}

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> GetUserAsync(string userId)
        => await _httpClient.GetFromJsonAsync<UserDto>($"api/user/{userId}");
}