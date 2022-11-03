using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
        => _context = context;

    //this should be authorized later by role
    public async Task<ApplicationUser?> GetApplicationUserById(string userId, CancellationToken ct)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);

    public async Task<decimal> GetBalanceAsync(string userId, CancellationToken ct)
    {
        var user = await GetApplicationUser(userId, ct);
        return user.Balance;
    }

    public async Task<decimal> DepositAsync(string userId, decimal balance, CancellationToken ct)
    {
        var user = await GetApplicationUser(userId, ct);
        user.Balance += balance;
        await _context.SaveChangesAsync(ct);
        return user.Balance;
    }

    public async Task<decimal> WithdrawAsync(string userId, decimal amount, CancellationToken ct)
    {
        var user = await GetApplicationUser(userId, ct);
        if (amount > user.Balance)
            throw new InvalidOperationException("Insufficient funds");
        user.Balance -= amount;
        await _context.SaveChangesAsync(ct);
        return user.Balance;
    }

    private async Task<ApplicationUser> GetApplicationUser(string userId, CancellationToken ct)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct) ??
           throw new InvalidOperationException("User does not exist");
}