using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Mappers;

public static class BetsMapper
{
    public static BetResponse ToBetDto(this BetEntity bet, OddsEntity odds)
        => new ()
        {
            Amount = bet.WagerAmount,
            PotentialWin = bet.WagerAmount * odds.Odds
        };
}