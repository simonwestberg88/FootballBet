using FootballBet.Shared.Models.Odds;

namespace FootballBet.Shared.Models.Bets;

public readonly record struct BetResponse
    (decimal WagerAmount, decimal PotentialWin, decimal PotentialBaseWin, MatchWinnerEnumDto MatchWinner);