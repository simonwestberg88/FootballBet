using FootballBet.Server.Models.Groups;
using System.Linq;

namespace FootballBet.Server.Data.Mappers
{
    public static class GroupMapper
    {
        public static Shared.Models.Groups.BettingGroupShared Map(BettingGroup bettingGroup)
            => new()
            {
                Description = bettingGroup.Description,
                Name = bettingGroup.Name,
                Id = bettingGroup.Id,
                Memberships = bettingGroup?.Memberships?.Select(x => Map(x))?.ToList()
            };

        public static Shared.Models.Groups.BettingGroupMemberShared Map(BettingGroupMember bettingGroupMember)
            => new()
            {
                Id = bettingGroupMember.Id,
                Nickname = bettingGroupMember.Nickname,
                UserId = bettingGroupMember.UserId
            };
    }
}
