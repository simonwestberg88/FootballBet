using FootballBet.Infrastructure.Interfaces;

namespace FootballBet.Server.Controllers;

public static class OddsApi
{
    public static void AddOddsApi(this WebApplication app)
    {
        app.MapGet("api/matches/odds", async (int matchId, IFootballApiClient client) => 
            await client.GetLatestOddsForMatch(matchId)).AllowAnonymous();
    }
}