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
    
    public async Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup)
    {
        await _context.OddsGroupEntities.AddAsync(matchOddsGroup);
        await _context.SaveChangesAsync();
        return matchOddsGroup.Id;
    }

    public async Task AddOddsAsync(IEnumerable<OddsEntity> oddsEntities)
    {
        await _context.OddsEntities.AddRangeAsync(oddsEntities);
        await _context.SaveChangesAsync();
    }
}