using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<BettingGroup> CreateGroup(ApplicationUser user, BettingGroup newGroup, CancellationToken ct);
        Task<List<BettingGroupMember>> GetBettingGroupMemberByUserId(string userId, CancellationToken ct);
        Task<BettingGroup> GetGroupById(Guid groupId, CancellationToken ct);
        Task JoinGroup(Guid groupId, BettingGroupMember member, CancellationToken ct);
        Task<BettingGroupInvitation> CreateBettingGroupInvitation(BettingGroupInvitation invitation, CancellationToken ct);
        Task<BettingGroupInvitation> GetBettingGroupInvitationByIdAsync(Guid invitationId, CancellationToken ct);
        Task DeleteBettingGroupInvitation(Guid invitationId, CancellationToken ct);
    }
}
