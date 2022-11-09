namespace FootballBet.Shared.Models.Bets;

public readonly record struct BetResponse
    (int OddsId, decimal Amount, decimal PotentialWin);