using FootballBet.Server.Models.Groups;
using FootballBet.Shared.Models.Groups;
using BettingGroupInvitation = FootballBet.Server.Models.Groups.BettingGroupInvitation;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct);
        public Task<BettingGroup> GetBettingGroupById(string id, CancellationToken ct);
        public Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct);
        public Task<BettingGroupInvitation> CreateInvitation(string groupId, string userEmail, string userId, CancellationToken ct);
        public Task ConsumeInvitation(string invitationId, string groupId, string userId, CancellationToken ct); 
    }
}
