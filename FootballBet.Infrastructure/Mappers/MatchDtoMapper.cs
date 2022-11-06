using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Match;

namespace FootballBet.Infrastructure.Mappers;

public static class MatchDtoMapper
{
    public static MatchDto ToMatchDto(this MatchEntity entity)
        => new()
        {
            MatchId = entity.Id,
            Date = entity.Date,
            Round = entity.Round,
            MatchStatus = entity.MatchStatus.ToStatusString(),
            AwayCurrentGoals = entity.AwayCurrentGoals,
            AwayFulltimeGoals = entity.AwayFulltimeGoals,
            AwayPenaltyGoals = entity.AwayFulltimeGoals,
            HomeTeamName = entity.HomeTeam.Name,
            AwayTeamName = entity.AwayTeam.Name,
            HomeCurrentGoals = entity.HomeCurrentGoals,
            HomeFulltimeGoals = entity.HomeFulltimeGoals,
            HomePenaltyGoals = entity.HomePenaltyGoals,
            HomeTeamLogo = entity.HomeTeam.LogoUrl,
            AwayTeamLogo = entity.AwayTeam.LogoUrl
        };
}