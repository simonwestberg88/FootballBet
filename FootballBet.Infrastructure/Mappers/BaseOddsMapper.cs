using System.Globalization;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;

namespace FootballBet.Infrastructure.Mappers;

public static class BaseOddsMapper
{
    public static BaseOddsEntity ToBaseOddsEntity(this BetValue betValue, int matchOddsGroupId)
        => new()
        {
            Odds = decimal.Parse(betValue.Odd, CultureInfo.InvariantCulture),
            MatchWinnerEntityEnum = ParseMatchWinner(betValue.Prediction.ToString()),
            MatchOddsGroupId = matchOddsGroupId
        };

    private static MatchWinnerEntityEnum ParseMatchWinner(string? prediction)
        => prediction switch
        {
            "Draw" => MatchWinnerEntityEnum.Draw,
            "Home" => MatchWinnerEntityEnum.Home,
            "Away" => MatchWinnerEntityEnum.Away,
            _ => throw new ArgumentOutOfRangeException(nameof(prediction), prediction, null)
        };
}