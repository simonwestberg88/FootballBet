using FootballBet.Infrastructure.Interfaces;

namespace TestProject.TestApi;

public static class ApiWebApplicationExtension
{
    public static void AddTestApi(this WebApplication app)
    {
        app.MapGet("/test/fixtures", async (IFootballApiClient client) =>
        {
            var fixtures = await client.GetFixtures(1, "2018");
            return fixtures;
        });
        
        app.MapGet("/test/leagues", async (IFootballApiClient client) =>
        {
            var league = await client.GetSpecificLeague("1");
            return league;
        });

        app.MapPost("test/seed", async (IFootballAPIService footballService) =>
        {
            await footballService.SeedDatabase("2022", 1);
        });
        
        app.MapGet("test/saveOdds", async (IFootballApiClient client) =>
        {
            await client.SaveOddsForLeague(1, "2022");
        });
        app.MapGet("test/match/{matchId:int}/odds", async (int matchId, IFootballApiClient client) =>
        {
            await client.GetLatestOddsForMatch(matchId);
        });
        
    }
}