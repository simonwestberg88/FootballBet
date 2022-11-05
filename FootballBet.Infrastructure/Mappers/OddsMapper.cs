using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Infrastructure.Mappers;

public static class OddsMapper
{
    public static OddsEntity ToOddsEntity(this BetValue betValue, int matchId)
    {
        var (homeGoals, awayGoals) = ParseExactResult(betValue.Prediction.ToString());
        
        return new OddsEntity
        {
            Created = DateTime.Now,
            Odds = decimal.Parse(betValue.Odd),
            MatchId = matchId,
            OddsType = ParseOddsType(betValue.Prediction.ToString()),
            HomeTeamScore = homeGoals,
            AwayTeamScore = awayGoals
        };
    }
    
    private static OddsType ParseOddsType(string? prediction)
    {
        return prediction switch
        {
            "Draw" => OddsType.Draw,
            "Home" => OddsType.HomeWin,
            "Away" => OddsType.AwayWin,
            _ => OddsType.ExactScore
        };
    }

    private static (int homeGoals, int awayGoals) ParseExactResult(string? prediction)
    {
        var split = prediction?.Split(':') ?? Array.Empty<string>();
        if (split.Length != 2) return (0, 0);
        var homeGoals = int.Parse(split?[0] ?? "0");
        var awayGoals = int.Parse(split?[1] ?? "0");
        return (homeGoals, awayGoals);
    }
}