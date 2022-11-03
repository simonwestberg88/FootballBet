using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Shared.Models.Http;

namespace FootballBet.Server.Controllers;

public static class UserApi
{
    public static void AddUserApi(this WebApplication app)
    {
        app.MapGet("/api/user/{userId}", async (string userId, IUserRepository userRepository, CancellationToken token) =>
        {
            var user = await userRepository.GetApplicationUserById(userId, token);
            return Results.Ok(user?.ToUserDto());
        });
        app.MapGet("/api/user/{userId}/balance", async (string userId, CancellationToken token, ITransactionService service) =>
        {
            try
            {
                return Results.Ok(await service.GetBalanceAsync(userId, token));
            }
            //todo change to not found exception
            catch (InvalidOperationException e)
            {
                return Results.NotFound(e.Message);
            }
        });
        
        app.MapPost("/api/user/{userId}/balance/withdraw", async (string userId, WithdrawRequest request, CancellationToken token, ITransactionService service) =>
        {
            try
            {
                return Results.Ok(await service.WithdrawAsync(userId, request.Amount, token));
            }
            //todo change to not found exception
            catch (InvalidOperationException e)
            {
                return Results.NotFound(e.Message);
            }
        });
        
        app.MapPost("/api/user/{userId}/balance/deposit", async (string userId, DepositRequest request, CancellationToken token, ITransactionService service) =>
        {
            try
            {
                return Results.Ok(await service.DepositAsync(userId, request.Amount, token));
            }
            //todo change to not found exception
            catch (InvalidOperationException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}