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
    public async Task<ApplicationUser?> GetUserAsync(string userId, CancellationToken ct)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);

    public async Task<decimal> GetBalanceAsync(string userId, string groupId, CancellationToken ct)
    {
        var user = await _context.UserBalanceEntities.FirstOrDefaultAsync(
            x => x.UserId == userId && x.GroupId == groupId, ct);
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }
        return user.Balance;
    }

    public async Task ChangeNicknameForGroupMemberAsync(string userId, string newNickname, string groupId)
    {
        var user = await _context.BettingGroupMembers.FirstOrDefaultAsync(x => x.UserId == userId && x.BettingGroupEntityId == Guid.Parse(groupId));
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }
        user.Nickname = newNickname;
        await _context.SaveChangesAsync();
    }

    public async Task<string> GetBettingGroupUserNicknameByUserIdAsync(string userId)
        => (await _context.BettingGroupMembers.FirstOrDefaultAsync(x => x.UserId == userId)).Nickname;

    public async Task<string> GetUserNickNameAsync(string userId, string groupId)
    {
        var user = await _context.BettingGroupMembers.FirstOrDefaultAsync(m => m.UserId == userId);
        return user?.Nickname ?? userId;
    }
}