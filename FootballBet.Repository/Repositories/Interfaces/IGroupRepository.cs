using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        Task<BettingGroupInvitationEntity> GetBettingGroupInvitationByIdAsync(Guid invitationId, CancellationToken ct);
        Task DeleteBettingGroupInvitation(Guid invitationId, CancellationToken ct);
    }
}
