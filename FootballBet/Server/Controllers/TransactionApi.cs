using FootballBet.Client.Services;

namespace FootballBet.Server.Controllers;

public static class TransactionApi
{
    public static void AddBalanceApi(this WebApplication app)
    {
        app.MapGet("/api/balance", async (string userId, ITransactionService service, CancellationToken token) =>
        {
            return await service.GetBalanceAsync(userId, token);
        });
    }
}