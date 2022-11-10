using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Shared.Models.Odds;
using System.Globalization;

namespace FootballBet.Infrastructure.Mappers;

public static class OddsMapper
{
    public static ExactScoreOddsDto ToOddsDto(this ExactScoreOddsEntity entity)
        => new ()
        {
            Id = entity.Id,
            Odds = entity.Odds,
            MatchWinner = MapMatchWinnerEnumDto(entity.MatchWinnerEntityEnum),
            AwayTeamGoals = entity.AwayTeamGoals,
            HomeTeamGoals = entity.HomeTeamGoals
        };

    public static MatchWinnerEnumDto MapMatchWinnerEnumDto(MatchWinnerEntityEnum matchWinnerEntityEnum)
        => matchWinnerEntityEnum switch
        {
            MatchWinnerEntityEnum.Away => MatchWinnerEnumDto.Away,
            MatchWinnerEntityEnum.Draw => MatchWinnerEnumDto.Draw,
            _ => MatchWinnerEnumDto.Home
        };

    public static ExactScoreOddsEntity ToOddsEntity(this BetValue betValue, int matchOddsGroupId)
    {
        var (homeGoals, awayGoals) = ParseExactResult(betValue.Prediction.ToString());
        return new ExactScoreOddsEntity
        {
            Odds = decimal.Parse(betValue.Odd, CultureInfo.InvariantCulture),
            MatchWinnerEntityEnum = ParseMatchWinnerFromExactScore(homeGoals, awayGoals),
            HomeTeamGoals = homeGoals,
            AwayTeamGoals = awayGoals,
            MatchOddsGroupId = matchOddsGroupId
        };
    }

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