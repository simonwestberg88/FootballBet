using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
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
    
    //add support for different groups

    public async Task<BetEntity> GetBetByIdAsync(int id)
        => await _context.BetEntities.FindAsync(id) ?? throw new InvalidOperationException("Bet not found");

    public async Task<IEnumerable<BetEntity>> GetBetsByUserIdAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new InvalidOperationException("User not found");
        return user.Bets;
    }

    public async Task<IEnumerable<BetEntity>> GetBetsByMatchIdAsync(int matchId)
    {
        var match = await _context.MatchEntities.FindAsync(matchId);
        if (match == null)
            throw new InvalidOperationException("Match not found");
        return match.Bets;
    }

    public Task<IEnumerable<BetEntity>> GetBetsByUserIdAndMatchIdAsync(int userId, int matchId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BetEntity>> GetBetsByUserIdAndMatchIdAsync(string userId, int matchId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.MatchId == matchId).ToListAsync();

    public Task<IEnumerable<BetEntity>> GetAllBetsAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteBetAsync(int id)
    {
        throw new NotImplementedException();
    }
}