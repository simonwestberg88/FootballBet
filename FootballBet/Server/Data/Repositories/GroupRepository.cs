using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GroupRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContextAccessor = httpContext;
        }

        public async Task<BettingGroup> CreateGroup(string creatorId, string description, string groupName, CancellationToken ct)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == creatorId);
            if (user == null)
                throw new KeyNotFoundException(); //better exception needed

            var newGroup = new BettingGroup()
            {
                Description = description,
                Name = groupName,
                Memberships = new List<BettingGroupMember>()
                {
                    new BettingGroupMember()
                    {
                        ApplicationUser = user,
                        Nickname = user.UserName,
                        UserId = user.Id
                    }
                }
            };

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
