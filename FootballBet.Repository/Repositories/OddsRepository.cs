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
        var groupId = _context.MatchOddsGroupEntities.OrderByDescending(x => x.Id)
            .Single(m => m.MatchId == matchId).Id;
        return await _context.ExactScoreOddsEntities.Where(x => x.MatchOddsGroupId == groupId).ToListAsync();
    }

    public async Task<BaseOddsEntity> GetLatestBaseOddsAsync(int matchId)
    {
        var groupId = _context.MatchOddsGroupEntities.OrderByDescending(x => x.Id)
            .Single(m => m.MatchId == matchId).Id;
        return await _context.BaseOddsEntities.SingleAsync(x => x.MatchOddsGroupId == groupId);
    }

    public async Task<ExactScoreOddsEntity?> GetOddsAsync(int oddsId)
        => await _context.ExactScoreOddsEntities.FindAsync(oddsId);

    public async Task<ExactScoreOddsEntity?> GetBaseOddsAsync(int oddsId, MatchWinnerEntityEnum winner)
    {
        var oddsEntity = await _context.ExactScoreOddsEntities.FindAsync(oddsId);
        if (oddsEntity == null)
            return null;
        var oddsGroupId = oddsEntity.MatchOddsGroupId;
        var baseOdds = _context.ExactScoreOddsEntities
            .Where(o => o.MatchOddsGroupId == oddsGroupId)
            .OrderBy(o => o.Odds)
            .FirstOrDefault(x => x.MatchWinnerEntityEnum == winner);
        return baseOdds;
    }
}