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
        
        //get top 3 wins for group
        app.MapGet("api/stats/topwins",
            async (string groupId, IStatsService service) =>
                await service.GetTop3WinStatsAsync(groupId)
        );
    }
}