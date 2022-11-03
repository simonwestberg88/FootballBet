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

    public async Task<decimal> UpdateBalance(string userId, decimal balance, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);
        if (user == null) return 0;
        user.Balance += balance;
        await _context.SaveChangesAsync(ct);
        return user.Balance;
    }
    
    
}