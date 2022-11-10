using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
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

    public async Task AddBaseOddsAsync(IEnumerable<BaseOddsEntity> baseOddsEntities)
    {
        await _context.BaseOddsEntities.AddRangeAsync(baseOddsEntities);
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddOddsGroupAsync(MatchOddsGroupEntity matchOddsGroup)
    {
        await _context.MatchOddsGroupEntities.AddAsync(matchOddsGroup);
        await _context.SaveChangesAsync();
        return matchOddsGroup.Id;
    }

    public async Task AddOddsAsync(IEnumerable<OddsEntity> oddsEntities)
    {
        await _context.OddsEntities.AddRangeAsync(oddsEntities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OddsEntity>> GetLatestOddsAsync(int matchId)
    {
        var groupId = _context.MatchOddsGroupEntities.OrderByDescending(x => x.Id)
            .Single(m => m.MatchId == matchId).Id;
        return await _context.OddsEntities.Where(x => x.MatchOddsGroupId == groupId).ToListAsync();
    }

    public async Task<OddsEntity?> GetOddsAsync(int oddsId)
        => await _context.OddsEntities.FindAsync(oddsId);

    public async Task<OddsEntity?> GetBaseOddsAsync(int oddsId, MatchWinnerEntityEnum winner)
    {
        var oddsEntity = await _context.OddsEntities.FindAsync(oddsId);
        if (oddsEntity == null)
            return null;
        var oddsGroupId = oddsEntity.MatchOddsGroupId;
        var baseOdds = _context.OddsEntities
            .Where(o => o.MatchOddsGroupId == oddsGroupId)
            .OrderBy(o => o.Odds)
            .FirstOrDefault(x => x.MatchWinnerEntityEnum == winner);
        return baseOdds;
    }
}