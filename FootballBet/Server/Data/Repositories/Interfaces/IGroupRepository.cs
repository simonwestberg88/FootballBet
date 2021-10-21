using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        public void JoinGroup(Guid groupId, Guid userId, CancellationToken ct);
        public Task<BettingGroup> CreateGroup(ApplicationUser user, BettingGroup newGroup, CancellationToken ct);
        public List<BettingGroup> ListGroups(CancellationToken ct);
    }
}
