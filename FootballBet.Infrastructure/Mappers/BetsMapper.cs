using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Mappers;

public static class BetsMapper
{
    public static BetResponse ToBetDto(this BetEntity bet, ExactScoreOddsEntity exactScoreOdds, BaseOddsEntity baseOdds)
        => new ()
        {
            WagerAmount = bet.WagerAmount,
            PotentialWin = bet.WagerAmount * exactScoreOdds.Odds,
            MatchWinner = OddsMapper.MapMatchWinnerEnumDto(exactScoreOdds.MatchWinnerEntityEnum),
            PotentialBaseWin = baseOdds.Odds * bet.WagerAmount,
            HomeGoals = exactScoreOdds.HomeTeamGoals,
            AwayGoals = exactScoreOdds.AwayTeamGoals
        };
}