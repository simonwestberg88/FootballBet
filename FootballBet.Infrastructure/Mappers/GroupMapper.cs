using FootballBet.Server.Models.Groups;

namespace FootballBet.Infrastructure.Mappers;

public static class GroupMapper
{
    public static Shared.Models.Groups.BettingGroupShared Map(BettingGroupEntity bettingGroupEntity)
        => new()
        {
            Description = bettingGroupEntity.Description,
            Name = bettingGroupEntity.Name,
            Id = bettingGroupEntity.Id,
            Memberships = bettingGroupEntity?.Memberships?.Select(x => Map(x))?.ToList()
        };

    public static Shared.Models.Groups.BettingGroupMemberShared Map(BettingGroupMemberEntity bettingGroupMemberEntity)
        => new()
        {
            Id = bettingGroupMemberEntity.Id,
            Nickname = bettingGroupMemberEntity.Nickname,
            UserId = bettingGroupMemberEntity.UserId
        };
}