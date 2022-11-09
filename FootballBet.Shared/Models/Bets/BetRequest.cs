namespace FootballBet.Shared.Models.Bets;

public readonly record struct BetRequest
(int OddsId, decimal Amount, string GroupId, int MatchId);

