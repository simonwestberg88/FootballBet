namespace FootballBet.Shared.Models.Odds;

public readonly record struct ExactScoreOddsDto
(
    int Id,
    MatchWinnerEnumDto MatchWinner,
    decimal Odds,
    int HomeTeamGoals,
    int AwayTeamGoals
);