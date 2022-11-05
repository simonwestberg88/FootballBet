namespace FootballBet.Shared.Models.Odds;

public readonly record struct OddsDto
(
    int Id,
    MatchWinnerEnumDto MatchWinner,
    decimal Odds,
    int HomeTeamGoals,
    int AwayTeamGoals
);

public enum MatchWinnerEnumDto{ Home, Away, Draw }