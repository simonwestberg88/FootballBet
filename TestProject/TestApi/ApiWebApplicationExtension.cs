using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Shared.Models.Bets;
using FootballBet.Shared.Models.Groups;

namespace TestProject.TestApi;

public static class ApiWebApplicationExtension
{
    public static void AddTestApi(this WebApplication app)
    {
        app.MapGet("/test/fixtures", async (IFootballApiClient client) =>
        {
            var fixtures = await client.GetFixtures(1, "2022");
            return fixtures;
        });

        app.MapGet("/test/matches", (IFootballRepository repository) =>
        {
            var matches = repository.GetAllMatchesForLeagueId(1);
            return matches;
        });

        app.MapGet("/test/leagues", async (IFootballApiClient client) =>
        {
            var league = await client.GetSpecificLeague("1");
            return league;
        });

        app.MapPost("test/seed",
            async (IFootballAPIService footballService) => { await footballService.SeedDatabase("2022", 1); });

        app.MapGet("test/saveOdds",
            async (IFootballApiClient client) => { await client.SaveOddsForLeague(1, "2022"); });
        app.MapGet("test/match/{matchId:int}/odds", async (int matchId, IFootballApiClient client)
            => await client.GetLatestOddsForMatch(matchId));

        app.MapPost("test/bets/place",
            async (string userId, string groupId, BetDto bet, IBetService service) =>
            {
                await service.PlaceBetAsync(bet.OddsId, userId, bet.Amount, groupId);
            });

        app.MapPost("test/bets",
            async (string userId, string groupId, IBetService service) =>
                await service.GetBetsForUserAsync(userId, groupId));

        app.MapPost("test/bettingGroup/create",
            async (string userId, BettingGroupShared bettingGroup, IGroupService service) =>
                await service.CreateBettingGroup(userId, bettingGroup.Description ?? "none", bettingGroup.Name,
                    CancellationToken.None));

        app.MapGet("/test/bettingGroup/all",
            async (string userId, IGroupService service) =>
                await service.ListGroupsForUser(userId, CancellationToken.None));
    }
}