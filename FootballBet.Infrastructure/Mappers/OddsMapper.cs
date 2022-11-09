using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Shared.Models.Odds;
using System.Globalization;

namespace FootballBet.Infrastructure.Mappers;

public static class OddsMapper
{
    public static OddsDto ToOddsDto(this OddsEntity entity)
        => new ()
        {
            Id = entity.Id,
            Odds = entity.Odds,
            MatchWinner = MapMatchWinnerEnumDto(entity.MatchWinnerEntityEnum),
            AwayTeamGoals = entity.AwayTeamGoals,
            HomeTeamGoals = entity.HomeTeamGoals
        };

    private static MatchWinnerEnumDto MapMatchWinnerEnumDto(MatchWinnerEntityEnum matchWinnerEntityEnum)
        => matchWinnerEntityEnum switch
        {
            MatchWinnerEntityEnum.Away => MatchWinnerEnumDto.Away,
            MatchWinnerEntityEnum.Draw => MatchWinnerEnumDto.Draw,
            _ => MatchWinnerEnumDto.Home
        };

    public static OddsEntity ToOddsEntity(this BetValue betValue, int matchId, int MatchOddsGroupId)
    {
        var (homeGoals, awayGoals) = ParseExactResult(betValue.Prediction.ToString());
        return new OddsEntity
        {
            Odds = decimal.Parse(betValue.Odd, CultureInfo.InvariantCulture),
            MatchWinnerEntityEnum = ParseMatchWinner(betValue.Prediction.ToString(), homeGoals, awayGoals),
            HomeTeamGoals = homeGoals,
            AwayTeamGoals = awayGoals,
            MatchOddsGroupId = MatchOddsGroupId
        };
    }

    private static MatchWinnerEntityEnum ParseMatchWinner(string? prediction, int homeGoals = 0, int awayGoals = 0)
        => prediction switch
        {
            "Draw" => MatchWinnerEntityEnum.Draw,
            "Home" => MatchWinnerEntityEnum.Home,
            "Away" => MatchWinnerEntityEnum.Away,
            _ => ParseMatchWinnerFromExactScore(homeGoals, awayGoals)
        };

    private static MatchWinnerEntityEnum ParseMatchWinnerFromExactScore(int homeGoals, int awayGoals)
        => homeGoals > awayGoals ? MatchWinnerEntityEnum.Home :
            homeGoals < awayGoals ? MatchWinnerEntityEnum.Away :
            MatchWinnerEntityEnum.Draw;


    private static (int homeGoals, int awayGoals) ParseExactResult(string? prediction)
    {
        var split = prediction?.Split(':') ?? Array.Empty<string>();
        if (split.Length != 2) return (0, 0);
        var homeGoals = int.Parse(split[0]);
        var awayGoals = int.Parse(split[1]);
        return (homeGoals, awayGoals);
    }
}