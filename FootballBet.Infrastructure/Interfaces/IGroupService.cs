using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Groups;

namespace FootballBet.Infrastructure.Interfaces;

public interface IGroupService
{
    public Task<BettingGroupEntity> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct);
    public Task<BettingGroupShared> GetBettingGroupById(string id, CancellationToken ct);
    public Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct);
    public Task<BettingGroupInvitationEntity> CreateInvitation(string groupId, string userEmail, string userId, CancellationToken ct);
    public Task ConsumeInvitation(string invitationId, string groupId, string userId, CancellationToken ct);
}