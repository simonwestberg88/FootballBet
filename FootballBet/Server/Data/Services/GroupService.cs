using FootballBet.Server.Data.Mappers;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Groups;
using FootballBet.Shared.Models.Groups;

namespace FootballBet.Server.Data.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task JoinGroup(Guid groupId, Guid userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<BettingGroup> CreateBettingGroup(string creatorId, string description, string groupName, CancellationToken ct)
        {
            var user = await _userRepository.GetApplicationUserById(creatorId, ct);
            if (user == null)
                throw new KeyNotFoundException(); //better exception needed

            var newGroup = CreateBettingGroup(groupName, description, user);
            return await _groupRepository.CreateGroup(user, newGroup, ct);
        }

        public async Task<List<BettingGroupShared>> ListGroupsForUser(string userId, CancellationToken ct)
        {
            var bettingGroupIds = (await _groupRepository.GetBettingGroupMemberByUserId(userId, ct)).Select(p => p.BettingGroupId)
                .ToList();
            var groupList = new List<BettingGroup>();

            foreach (var id in bettingGroupIds)
            {
                groupList.Add(await _groupRepository.GetGroupById(id, ct));
            }
            return groupList.Select(g => GroupMapper.Map(g)).ToList();
        }

        private static BettingGroup CreateBettingGroup(string groupName, string description, ApplicationUser creator)
           => new()
           {
               Description = description,
               Name = groupName,
               Memberships = new List<BettingGroupMember>()
                    {
                        new BettingGroupMember()
                        {
                            ApplicationUser = creator,
                            Nickname = creator.UserName,
                            UserId = creator.Id
                        }
                    },
               CreatedAt = DateTime.UtcNow
           };
    }
}
