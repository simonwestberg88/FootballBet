namespace FootballBet.Shared.Models.Bets;

public readonly record struct BetDto
(int OddsId, string UserId, decimal Amount, string BettingGroupId);