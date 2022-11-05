using FootballBet.Infrastructure.Interfaces;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Server.Controllers;

public static class BetApi
{
    public static void AddBetApi(this WebApplication app)
    {
        app.MapPost("api/bets/place",
            async (string userId, string groupId, BetDto bet, IBetService service) =>
                await service.PlaceBetAsync(bet.OddsId, userId, bet.Amount, groupId));

        app.MapGet("api/bets",
            async (string userId, string groupId, IBetService service) =>
                await service.GetBets(userId, groupId));
    }
}