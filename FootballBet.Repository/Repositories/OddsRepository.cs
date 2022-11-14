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

    public async Task AddOddsAsync(IEnumerable<ExactScoreOddsEntity> oddsEntities)
    {
        await _context.ExactScoreOddsEntities.AddRangeAsync(oddsEntities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ExactScoreOddsEntity>> GetLatestExactScoreOddsAsync(int matchId)
    {
        var group = await _context.MatchOddsGroupEntities.OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(m => m.MatchId == matchId);
        if(group is null) return new List<ExactScoreOddsEntity>();
        return await _context.ExactScoreOddsEntities.Where(x => x.MatchOddsGroupId == group.Id).ToListAsync();
    }

    public async Task<IEnumerable<BaseOddsEntity>> GetLatestBaseOddsAsync(int matchId)
    {
        var group = await _context.MatchOddsGroupEntities.OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(m => m.MatchId == matchId);
        if(group is null) return new List<BaseOddsEntity>();
        return await _context.BaseOddsEntities.Where(x => x.MatchOddsGroupId == group.Id).ToListAsync();
    }

    public async Task<ExactScoreOddsEntity?> GetOddsAsync(int exactOddsId)
        => await _context.ExactScoreOddsEntities.FindAsync(exactOddsId);

    public async Task<BaseOddsEntity?> GetBaseOddsAsync(int exactOddsId, MatchWinnerEntityEnum winner)
    {
        var exactScoreOddsEntity = await _context.ExactScoreOddsEntities.FindAsync(exactOddsId);
        if (exactScoreOddsEntity == null)
            return null;
        var oddsGroupId = exactScoreOddsEntity.MatchOddsGroupId;
        var baseOdds = _context.BaseOddsEntities
            .Where(o => o.MatchOddsGroupId == oddsGroupId)
            .FirstOrDefault(x => x.MatchWinnerEntityEnum == winner);
        return baseOdds;
    }
}