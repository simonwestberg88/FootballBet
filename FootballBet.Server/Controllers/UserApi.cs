using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Server.Controllers;

public static class UserApi
{
    public static void AddUserApi(this WebApplication app)
    {
        app.MapGet("/api/user/{userId}", async (string userId, IUserRepository userRepository, CancellationToken token) =>
        {
            var user = await userRepository.GetUserAsync(userId, token);
            return Results.Ok(user?.ToUserDto());
        });
    }
}