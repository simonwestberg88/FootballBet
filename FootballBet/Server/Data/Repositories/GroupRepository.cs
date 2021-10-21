using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;

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

        public void JoinGroup(Guid groupId, Guid userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public List<BettingGroup> ListGroups(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
