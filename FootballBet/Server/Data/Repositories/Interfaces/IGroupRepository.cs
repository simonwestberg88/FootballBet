using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        public void JoinGroup(Guid groupId, Guid userId, CancellationToken ct);
        public Task<BettingGroup> CreateGroup(string creatorId, string description, string groupName, CancellationToken ct);
        public List<BettingGroup> ListGroups(CancellationToken ct);
    }
}
