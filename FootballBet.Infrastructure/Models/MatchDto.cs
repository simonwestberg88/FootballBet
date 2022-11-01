using FootballBet.Repository.Entities;

namespace FootballBet.Infrastructure.Models;

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
 MatchStatus MatchStatus, 
 string Round);