using FootballBet.Server.Data.Mappers;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models;
using FootballBet.Shared.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FootballBet.Server.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
            => _context = context;

        //this should be authorized later by role
        public async Task<ApplicationUser> GetApplicationUserById(string userId, CancellationToken ct)
            => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);

        public async Task<User> GetUserById(string userId, CancellationToken ct)
            => UserMapper.Map(await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct));
        
    }
}
