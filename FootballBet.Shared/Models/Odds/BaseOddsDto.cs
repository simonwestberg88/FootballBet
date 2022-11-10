namespace FootballBet.Shared.Models.Odds;

public readonly record struct BaseOddsDto
(
    int Id,
    MatchWinnerEnumDto MatchWinner,
    decimal Odds
);