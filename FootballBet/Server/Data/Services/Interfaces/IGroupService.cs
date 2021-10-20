using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IGroupService
    {
        public void JoinGroup(Guid groupId, Guid userId, CancellationToken ct);
        public List<BettingGroup> ListGroups(CancellationToken ct);
        public Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct);
    }
}
