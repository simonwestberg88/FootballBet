using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Server.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext context)
         => _context = context;

        public async Task<BettingGroup> CreateGroup(ApplicationUser user, BettingGroup newGroup, CancellationToken ct)
        {
            await _context.BettingGroups.AddAsync(newGroup, ct);
            await _context.SaveChangesAsync(ct);
            return newGroup;
        }


        public async Task<List<BettingGroupMember>> GetBettingGroupMemberByUserId(string userId, CancellationToken ct)
            => await _context.BettingGroupMembers.Where(x => x.UserId == userId).ToListAsync(ct);

        public async Task JoinGroup(Guid groupId, Guid userId, CancellationToken ct)
        {
            var group = await _context.BettingGroups.FirstOrDefaultAsync(x => x.Id == groupId, ct);
            var member = await _context.BettingGroupMembers.FirstOrDefaultAsync(x => x.UserId == userId.ToString(), ct);
            if (member != null && group != null)
            {
                group.Memberships.Add(member);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<BettingGroup> GetGroupById(Guid groupId, CancellationToken ct)
            => await _context.BettingGroups.FirstOrDefaultAsync(x => x.Id == groupId, ct);

        public async Task<BettingGroupInvitation> CreateBettingGroupInvitation(BettingGroupInvitation invitation, CancellationToken ct)
        {
            await _context.BettingGroupInvitations.AddAsync(invitation, ct);
            return invitation;
        }

        //public async Task<BettingGroupInvitation> GetBettingGroupInvitationByUserIdAsync
    }
}
