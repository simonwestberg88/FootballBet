using FootballBet.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public interface IStatRepository
{
    public Task<IEnumerable<WinEntity>> GetWinsAsync(string groupId);
    public Task<IEnumerable<WinEntity>> GetTop3WinsAsync(string groupId);
    public Task<IEnumerable<WinEntity>> GetWinsAsync(string groupId, string userId);
}

public class StatRepository : IStatRepository
{
    private readonly ApplicationDbContext _context;

    public StatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WinEntity>> GetWinsAsync(string groupId)
        => await _context.WinEntities.Where(w => w.GroupId == groupId).ToListAsync();

    public async Task<IEnumerable<WinEntity>> GetTop3WinsAsync(string groupId)
        => await _context.WinEntities.Where(w => w.GroupId == groupId)
            .OrderByDescending(x => x.Amount).Take(3)
            .ToListAsync();

    public async Task<IEnumerable<WinEntity>> GetWinsAsync(string groupId, string userId)
        => await _context.WinEntities.Where(w => w.GroupId == groupId && w.UserId == userId).ToListAsync();
}