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

        app.MapGet("/test/matches/all", (int leagueId, IFootballRepository repository) =>
            repository.GetAllMatches(leagueId));
        
        app.MapGet("/test/matches", (int matchId, IMatchRepository repository) =>
            repository.GetMatchAsync(matchId));

        app.MapGet("/test/leagues", (IFootballApiClient client) =>
            client.GetSpecificLeague("1"));

        app.MapPost("test/seed/matches",
            async (string season, int leagueId, IFootballAPIService footballService) =>
            {
                await footballService.SeedDatabase(season, leagueId);
            });

        app.MapPost("test/seed/odds",
            async (string season, int leagueId, IFootballApiClient client) =>
            {
                await client.SaveOddsForLeague(leagueId, season);
            });

        app.MapGet("test/odds", async (int matchId, IFootballApiClient client)
            => await client.GetLatestExactScoreOdds(matchId));

        app.MapPost("test/bets/place",
            async (string userId, int matchId, string groupId, BetRequest bet, IBetService service) =>
                await service.PlaceBetAsync(bet.OddsId, matchId, userId, bet.Amount, groupId));

        app.MapGet("test/bets",
            async (string userId, string groupId, IBetService service) =>
                await service.GetBets(userId, groupId));

        app.MapPost("test/bettingGroup/create",
            async (string userId, BettingGroupShared bettingGroup, IGroupService service) =>
                await service.CreateBettingGroup(userId, bettingGroup.Description ?? "none", bettingGroup.Name,
                    CancellationToken.None));

        app.MapGet("/test/bettingGroup/all",
            async (string userId, IGroupService service) =>
                await service.ListGroupsForUser(userId, CancellationToken.None));
    }
}