using FootballBet.Client.Services;
using FootballBet.Shared.Models.Http;

namespace FootballBet.Server.Controllers;

public static class UserApi
{
    public static void AddUserApi(this WebApplication app)
    {
        app.MapGet("/api/{userId}/balance", async (string userId, ITransactionService service, CancellationToken token) =>
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
        
        app.MapPost("/api/{userId}/balance/withdraw", async (string userId, TransactionRequest request, ITransactionService service, CancellationToken token) =>
        {
            try
            {
                return Results.Ok(await service.WithdrawAsync(userId, request.WithdrawAmount));
            }
            //todo change to not found exception
            catch (InvalidOperationException e)
            {
                return Results.NotFound(e.Message);
            }
        });
        
        app.MapPost("/api/{userId}/balance/deposit", async (string userId, TransactionRequest request, ITransactionService service, CancellationToken token) =>
        {
            try
            {
                return Results.Ok(await service.DepositAsync(userId, request.DepositAmount));
            }
            //todo change to not found exception
            catch (InvalidOperationException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}