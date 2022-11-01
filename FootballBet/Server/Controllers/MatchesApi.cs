using FootballBet.Infrastructure.Interfaces;

namespace FootballBet.Server.Controllers;

public static class MatchesApi
{
    public static void AddMatchesApi(this WebApplication app)
    {
        app.MapGet("/matches/{leagueId:int}", (int leagueId, IFootballAPIService footballApiService) =>
             footballApiService.GetMatches(leagueId)
        );
    }
}