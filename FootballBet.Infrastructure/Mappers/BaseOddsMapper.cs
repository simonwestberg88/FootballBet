using System.Globalization;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Shared.Models.Odds;

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
    
    public static BaseOddsDto ToBaseOddsDto(this BaseOddsEntity baseOddsEntity)
        => new()
        {
            Odds = baseOddsEntity.Odds,
            MatchWinner = (MatchWinnerEnumDto)baseOddsEntity.MatchWinnerEntityEnum
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