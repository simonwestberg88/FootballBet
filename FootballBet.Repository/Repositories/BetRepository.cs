using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class BetRepository : IBetRepository
{
    private readonly ApplicationDbContext _context;

    public BetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BetEntity> GetBetByIdAsync(int id)
        => await _context.BetEntities.FindAsync(id) ?? throw new InvalidOperationException("Bet not found");

    public async Task<IEnumerable<BetEntity>> GetBetsByUserIdAsync(string userId, string bettingGroupId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.BettingGroupId == bettingGroupId)
            .ToListAsync();

    public async Task<IEnumerable<BetEntity>> GetBetsByMatchIdAsync(int matchId, string bettingGroupId)
        => await _context.BetEntities.Where(b => b.MatchId == matchId && b.BettingGroupId == bettingGroupId)
            .ToListAsync();

    public async Task<IEnumerable<BetEntity>> GetBetsByUserIdAndMatchIdAsync(string userId, int matchId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.MatchId == matchId).ToListAsync();

    public async Task PlaceBetAsync(BetEntity bet)
        => await _context.BetEntities.AddAsync(bet);
}