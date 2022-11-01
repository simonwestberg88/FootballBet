namespace FootballBet.Shared.Models.Match;

 public record struct MatchDto (
 DateTime Date, 
 int? HomeFulltimeGoals, 
 int? AwayFulltimeGoals, 
 int? HomeCurrentGoals, 
 int? AwayCurrentGoals, 
 int? HomePenaltyGoals, 
 int? AwayPenaltyGoals, 
 string? AwayTeamName, 
 string? HomeTeamName,
 string? HomeTeamLogo,
 string? AwayTeamLogo,
 string MatchStatus, 
 string Round);