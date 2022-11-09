using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Mappers;

public static class BetsMapper
{
    public static BetResponse ToBetDto(this BetEntity bet, OddsEntity odds, OddsEntity baseOdds)
        => new ()
        {
            WagerAmount = bet.WagerAmount,
            PotentialWin = bet.WagerAmount * odds.Odds,
            MatchWinner = OddsMapper.MapMatchWinnerEnumDto(odds.MatchWinnerEntityEnum),
            PotentialBaseWin = baseOdds.Odds * bet.WagerAmount
        };
}