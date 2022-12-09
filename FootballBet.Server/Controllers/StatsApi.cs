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
        
        //get top 10 wins for group
        app.MapGet("api/stats/topwins",
            async (string groupId, IStatsService service) =>
                await service.GetTop10WinStatsAsync(groupId)
        );
        
        app.MapGet("api/stats/latestwins",
            async (string groupId, IStatsService service) =>
                await service.GetLatestWinsAsync(groupId)
        );
        
        app.MapGet("api/stats/chart",
            async (string groupId, IStatsService service) =>
                await service.GetChartStatsAsync(groupId)
        );
    }
}