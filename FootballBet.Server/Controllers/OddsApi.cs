using FootballBet.Infrastructure.Services;
using FootballBet.Shared.Models.Odds;
using Microsoft.Extensions.Caching.Memory;

namespace FootballBet.Server.Controllers;

public static class OddsApi
{
    public static void AddOddsApi(this WebApplication app)
    {
        app.MapGet("api/matches/odds", async (int matchId, IOddsService service, IMemoryCache cache) =>
        {
            if (cache.TryGetValue(matchId, out IEnumerable<ExactScoreOddsDto> cachedOdds))
            {
                return cachedOdds;
            }

            var odds = (await service.GetLatestExactOddsAsync(matchId)).ToList();
            cache.Set(matchId, odds, TimeSpan.FromHours(1));
            return odds;
        }).AllowAnonymous();
        
        app.MapGet("api/matches/baseOdds", async (int matchId, IOddsService service, IMemoryCache cache) =>
        {
            if (cache.TryGetValue(matchId, out BaseOddsResponse cachedOdds))
            {
                return cachedOdds;
            }

            var odds = await service.GetLatestBaseOddsAsync(matchId);
            cache.Set(matchId, odds, TimeSpan.FromHours(1));
            return odds;
        }).AllowAnonymous();
    }
}