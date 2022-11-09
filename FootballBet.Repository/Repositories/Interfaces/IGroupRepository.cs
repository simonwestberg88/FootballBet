using FootballBet.Repository.Entities;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<BettingGroupEntity> CreateGroup(ApplicationUser user, BettingGroupEntity newGroupEntity, CancellationToken ct);
        Task<List<BettingGroupMemberEntity>> GetBettingGroupMemberByUserId(string userId, CancellationToken ct);
        Task<BettingGroupEntity> GetGroupById(Guid groupId, CancellationToken ct);
        Task JoinGroup(Guid groupId, BettingGroupMemberEntity memberEntity, CancellationToken ct);
        Task<BettingGroupInvitationEntity> CreateBettingGroupInvitation(BettingGroupInvitationEntity invitationEntity, CancellationToken ct);
        Task DeleteBettingGroupInvitationsByEmailAndGroupId(string email, string groupId);
        Task<BettingGroupInvitationEntity> GetBettingGroupInvitationByIdAsync(Guid invitationId, CancellationToken ct);
        Task DeleteBettingGroupInvitation(Guid invitationId, CancellationToken ct);
    }
}
