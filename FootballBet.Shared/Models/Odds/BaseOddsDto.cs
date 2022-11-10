namespace FootballBet.Shared.Models.Odds;

public readonly record struct BaseOddsDto
(
    int Id,
    MatchWinnerEnumDto MatchWinner,
    decimal Odds
);

public readonly record struct BaseOddsResponse(
    BaseOddsDto Home,
    BaseOddsDto Draw,
    BaseOddsDto Away
);
