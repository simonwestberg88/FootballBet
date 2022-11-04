using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Groups;

namespace FootballBet.Infrastructure.Mappers;

public static class GroupMapper
{
    public static BettingGroupShared Map(BettingGroupEntity bettingGroupEntity)
        => new()
        {
            Description = bettingGroupEntity.Description,
            Name = bettingGroupEntity.Name,
            Id = bettingGroupEntity.Id,
            Memberships = bettingGroupEntity?.Memberships?.Select(x => Map(x))?.ToList(),
            League = Map(bettingGroupEntity?.League)
        };

    public static LeagueShared Map(LeagueEntity leagueEntity)
        => new()
        {
            LogoUrl = leagueEntity?.LogoUrl,
            Name = leagueEntity?.Name,
            Season = leagueEntity?.Season
        };

    public static BettingGroupMemberShared Map(BettingGroupMemberEntity bettingGroupMemberEntity)
        => new()
        {
            Id = bettingGroupMemberEntity.Id,
            Nickname = bettingGroupMemberEntity.Nickname,
            UserId = bettingGroupMemberEntity.UserId
        };
}