using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class OddsRepository : IOddsRepository
{
    private readonly ApplicationDbContext _context;

    public OddsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOddsAsync(IEnumerable<OddsEntity> oddsList)
    {
        await _context.OddsEntities.AddRangeAsync(oddsList);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OddsEntity>> GetOddsForMatchAsync(int matchId)
        => await _context.OddsEntities.Where(x => x.MatchId == matchId).ToListAsync();
}