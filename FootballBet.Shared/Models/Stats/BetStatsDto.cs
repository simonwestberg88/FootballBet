namespace FootballBet.Shared.Models.Stats;

public readonly record struct BetStatsDto(decimal Balance, int ExactWins, int BaseWins, int Losses);