using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly ApplicationDbContext _context;

    public MatchRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MatchEntity>> GetMatchesForLeague(int leagueId)
        => await _context.MatchEntities.Where(m => m.LeagueId == leagueId).ToListAsync();

    public async Task<MatchEntity?> GetMatchAsync(int matchId)
        => await _context.MatchEntities.SingleOrDefaultAsync(m => m.Id == matchId);
}