using FootballBet.Repository.Entities;
using FootballBet.Server.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _context;
    public GroupRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<BettingGroupEntity> CreateGroup(ApplicationUser user, BettingGroupEntity newGroupEntity, CancellationToken ct)
    {
        await _context.BettingGroups.AddAsync(newGroupEntity, ct);
        await AddUserBalanceAsync(user.Id, newGroupEntity.Id.ToString(), ct);
        await _context.SaveChangesAsync(ct);
        return newGroupEntity;
    }

    private async Task AddUserBalanceAsync(string userId, string groupId, CancellationToken ct)
    {
        var userBalance = new UserBalanceEntity
        {
            Balance = 0,
            GroupId = groupId,
            UserId = userId
        };
        await _context.UserBalanceEntities.AddAsync(userBalance, ct);
    }


    public async Task<List<BettingGroupMemberEntity>> GetBettingGroupMemberByUserId(string userId, CancellationToken ct)
        => await _context.BettingGroupMembers.Where(x => x.UserId == userId).ToListAsync(ct);

    public async Task JoinGroup(Guid groupId, BettingGroupMemberEntity memberEntity, CancellationToken ct)
    {
        var group = await _context.BettingGroups.Include(group => group.Memberships).FirstOrDefaultAsync(x => x.Id == groupId, ct); //need to test if we can do this part in the service and use a parameter instead
        if (memberEntity != null && group != null)
        {
            group.Memberships.Add(memberEntity);
            await AddUserBalanceAsync(memberEntity.UserId, groupId.ToString(), ct);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<BettingGroupEntity> GetGroupById(Guid groupId, CancellationToken ct)
        => await _context.BettingGroups.Include(group => group.Memberships).ThenInclude(m => m.ApplicationUser).Include(group => group.League).FirstOrDefaultAsync(x => x.Id == groupId, ct);

    public async Task<BettingGroupInvitationEntity> CreateBettingGroupInvitation(BettingGroupInvitationEntity invitationEntity, CancellationToken ct)
    {
        await _context.BettingGroupInvitations.AddAsync(invitationEntity, ct);
        await _context.SaveChangesAsync(ct);
        return invitationEntity;
    }

    public async Task DeleteBettingGroupInvitationsByEmailAndGroupId(string email, string groupId)
    {
        _context.BettingGroupInvitations.RemoveRange(_context.BettingGroupInvitations.Where(i => i.InvitedUserEmail == email && i.BettingGroupEntityId.ToString() == groupId));
        await _context.SaveChangesAsync();
    }
    public async Task<BettingGroupInvitationEntity> GetBettingGroupInvitationByIdAsync(Guid invitationId, CancellationToken ct)
        => await _context.BettingGroupInvitations.FirstOrDefaultAsync(x => x.BettingGroupInvitationId == invitationId, ct);

    public async Task DeleteBettingGroupInvitation(Guid invitationId, CancellationToken ct)
    {
        var invitation = await _context.BettingGroupInvitations.FirstOrDefaultAsync(x => x.BettingGroupInvitationId == invitationId, ct);
        if (invitation != null)
        {
            _context.BettingGroupInvitations.Remove(invitation);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<List<BettingGroupMemberEntity>> GetBettingGroupMembersAsync(string groupId)
        => (await _context.BettingGroups.Include(b => b.Memberships).FirstOrDefaultAsync(bg => bg.Id == Guid.Parse(groupId)))?.Memberships;

}