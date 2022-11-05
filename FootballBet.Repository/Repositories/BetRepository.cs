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

    public async Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, string bettingGroupId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.BettingGroupId == bettingGroupId)
            .ToListAsync();

    public async Task<IEnumerable<BetEntity>> GetBetsAsync(int matchId, string bettingGroupId)
        => await _context.BetEntities.Where(b => b.MatchId == matchId && b.BettingGroupId == bettingGroupId)
            .ToListAsync();

    public async Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, int matchId, string groupId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.MatchId == matchId && b.BettingGroupId == groupId).ToListAsync();

    public async Task<BetEntity> PlaceBetAsync(BetEntity bet)
    {
        await _context.BetEntities.AddAsync(bet);
        await _context.SaveChangesAsync();
        return bet;
    }
}