using FootballBet.Infrastructure.Interfaces;
using FootballBet.Shared.Models.Odds;
using Microsoft.Extensions.Caching.Memory;

namespace FootballBet.Server.Controllers;

public static class OddsApi
{
    public static void AddOddsApi(this WebApplication app)
    {
        app.MapGet("api/matches/odds", async (int matchId, IFootballApiClient client, IMemoryCache cache) =>
        {
            if (cache.TryGetValue(matchId, out IEnumerable<ExactScoreOddsDto> cachedOdds))
            {
                return cachedOdds;
            }

            var odds = (await client.GetLatestExactScoreOdds(matchId)).ToList();
            cache.Set(matchId, odds, TimeSpan.FromHours(1));
            return odds;
        }).AllowAnonymous();
    }
}