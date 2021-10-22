using FootballBet.Server.Models.Groups;
using FootballBet.Shared.Models.Groups;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct);
        public Task JoinGroup(Guid groupId, Guid userId, CancellationToken ct);
        public Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct);
    }
}
