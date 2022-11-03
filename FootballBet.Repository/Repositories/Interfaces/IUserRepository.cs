using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetApplicationUserById(string userId, CancellationToken ct);
    public Task<double> UpdateBalance(string userId, double balance, CancellationToken ct);
}