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
        => throw new NotImplementedException();

    public async Task<BetEntity?> GetBetAsync(string userId, int matchId, string groupId)
    {
        var bet = await _context.BetEntities.SingleOrDefaultAsync(
            b => b.UserId == userId && b.BettingGroupId == groupId && b.MatchId == matchId);
        return bet;
    }

    public async Task<BetEntity> PlaceBetAsync(BetEntity bet)
    {
        //check if bet for user and match already exists
        var existingBet = await _context.BetEntities.SingleOrDefaultAsync(b =>
            b.UserId == bet.UserId && b.MatchId == bet.MatchId && b.BettingGroupId == bet.BettingGroupId);
        if(existingBet is not null)
            throw new InvalidOperationException("Bet already exists");
        await _context.BetEntities.AddAsync(bet);
        await _context.SaveChangesAsync();
        return bet;
    }
}