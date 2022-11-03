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
            await footballService.SeedDatabase();
        });
        
        app.MapGet("test/odds", async (IFootballApiClient client) =>
        {
            await client.GetOddsForLeague(1, "2022");
        });
    }
}