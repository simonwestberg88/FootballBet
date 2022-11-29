using System.Security.Claims;
using FootballBet.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity;

namespace FootballBet.Server.Controllers;

public static class StatsApi
{
    public static void AddstatsApi(this WebApplication app)
    {
        app.MapGet("api/stats/appbar",
            async (string groupId, IStatsService service, ClaimsPrincipal user) =>
                await service.GetAppBarStatsAsync(groupId, user.Identity.GetUserId())
        );
    }
}