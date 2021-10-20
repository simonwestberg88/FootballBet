using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        public GroupService(IGroupRepository repository)
            => _repository = repository;

        public void JoinGroup(Guid groupId, Guid userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct)
            => await _repository.CreateGroup(creatorId, description, groupName, ct);

        public List<BettingGroup> ListGroups(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
