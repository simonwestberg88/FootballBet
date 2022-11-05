using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Repository.Repositories;

public class OddsRepository : IOddsRepository
{
    private readonly ApplicationDbContext _context;
    public OddsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOddsAsync(IEnumerable<OddsEntity> oddsEntities)
    {
        await _context.OddsEntities.AddRangeAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}