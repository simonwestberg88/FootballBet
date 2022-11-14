using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Services;
using FootballBet.Shared.Models.Match;
using Microsoft.Extensions.Caching.Memory;

namespace FootballBet.Server.Controllers;

public static class MatchesApi
{
    public static void AddMatchesApi(this WebApplication app)
    {
        app.MapGet("api/matches/{leagueId:int}",
            (int leagueId, IMemoryCache cache, IMatchService matchService) =>
            {
                if(cache.TryGetValue(leagueId, out List<MatchDto> cachedMatches))
                {
                    return cachedMatches;
                }
                var matches = matchService.GetMatches(leagueId).ToList();
                cache.Set(leagueId, matches, TimeSpan.FromMinutes(1));
                return matches;
            }).AllowAnonymous();
    }
}