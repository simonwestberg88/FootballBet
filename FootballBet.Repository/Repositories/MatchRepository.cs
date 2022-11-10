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

    public async Task<IEnumerable<MatchEntity>> GetMatches(int leagueId)
        => await _context.MatchEntities.Where(m => m.LeagueId == leagueId).ToListAsync();

    public async Task<MatchEntity?> GetMatchAsync(int matchId)
        => await _context.MatchEntities.SingleOrDefaultAsync(m => m.Id == matchId);

    public async Task<IEnumerable<MatchEntity>> GetUnprocessedMatchesAsync()
    {
        var finishedStatus = new List<int>
        {
            (int)MatchStatus.FT,
            (int)MatchStatus.AET,
            (int)MatchStatus.PEN
        };
        var test = await _context.MatchEntities.FirstOrDefaultAsync();
        var testFinished = finishedStatus.Any(f => f == (int)test.MatchStatus);
        var finished = await _context.MatchEntities
            .Where(m =>
                finishedStatus.Any(f => f == (int)m.MatchStatus)
                && m.Date < DateTime.Now 
                && m.BetsPayed == false)
            .ToListAsync();
        return finished;
    }
}