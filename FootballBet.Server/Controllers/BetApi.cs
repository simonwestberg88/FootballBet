using System.Security.Claims;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Shared.Models.Bets;
using Microsoft.AspNet.Identity;

namespace FootballBet.Server.Controllers;

public static class BetApi
{
    public static void AddBetApi(this WebApplication app)
    {
        app.MapPost("api/bets/place",
            async (BetRequest bet, IBetService service, ClaimsPrincipal user) =>
                await service.PlaceBetAsync(bet.OddsId,  bet.MatchId, user.Identity.GetUserId(), bet.Amount, bet.GroupId));

        app.MapGet("api/bet/match",
            async (int matchId, string groupId, IBetService service, ClaimsPrincipal user) => 
                await service.GetBet(user.Identity.GetUserId(), matchId, groupId));

        app.MapGet("api/bets",
            async (string groupId, IBetService service, ClaimsPrincipal user) =>
                await service.GetBets(user.Identity.GetUserId(), groupId));
    }
}