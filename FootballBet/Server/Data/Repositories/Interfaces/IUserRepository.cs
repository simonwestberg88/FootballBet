using FootballBet.Server.Models;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(string userId, CancellationToken ct);
        public Task<ApplicationUser> GetApplicationUserById(string userId, CancellationToken ct);
    }
}
