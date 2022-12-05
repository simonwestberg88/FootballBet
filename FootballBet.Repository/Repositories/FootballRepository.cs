using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class FootballRepository : IFootballRepository
{
    private readonly ApplicationDbContext _context;

    public FootballRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<LeagueEntity> CreateOrUpdateLeague(LeagueEntity league)
    {
        var entity = await _context.LeagueEntities.FindAsync(league.Id);
        if (entity == null)
            await _context.LeagueEntities.AddAsync(league);
        else
        {
            entity.Season = league.Season;
            entity.Matches = league.Matches;
            entity.LogoUrl = league.LogoUrl;
            _context.LeagueEntities.Update(entity);
        }

        await _context.SaveChangesAsync();
        return league;
    }

    public async Task<(int Updated, int Created)> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams)
    {
        var updated = 0;
        var created = 0;
        foreach (var team in teams)
        {
            var entity = await _context.TeamEntities.FindAsync(team.Id);
            if (entity == null)
            {
                await _context.TeamEntities.AddAsync(team);
                created++;
            }
            else if (entity.LogoUrl != team.LogoUrl || entity.Name != team.Name)
            {
                entity.LogoUrl = team.LogoUrl;
                entity.Name = team.Name;
                _context.TeamEntities.Update(entity);
                updated++;
            }
        }

        await _context.SaveChangesAsync();
        return (Updated: updated, Created: created);
    }

    public async Task<(int Updated, int Created)> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches)
    {
        var updated = 0;
        var created = 0;
        foreach (var match in matches)
        {
            var entity = await _context.MatchEntities.FindAsync(match.Id);
            if (entity == null)
            {
                await _context.MatchEntities.AddAsync(match);
                created++;
            }
            else if (entity.HomeTeamId != match.HomeTeamId ||
                     entity.AwayTeamId != match.AwayTeamId ||
                     entity.AwayCurrentGoals != match.AwayCurrentGoals ||
                     entity.HomeCurrentGoals != match.HomeCurrentGoals ||
                     entity.AwayFulltimeGoals != match.AwayFulltimeGoals ||
                     entity.HomeFulltimeGoals != match.HomeFulltimeGoals ||
                     entity.AwayPenaltyGoals != match.AwayPenaltyGoals ||
                     entity.HomePenaltyGoals != match.HomePenaltyGoals ||
                     entity.MatchStatus != match.MatchStatus ||
                     entity.Season != match.Season ||
                     entity.LeagueId != match.LeagueId ||
                     entity.Round != match.Round ||
                     entity.Date != match.Date)
            {
                entity.HomeTeamId = match.HomeTeamId;
                entity.AwayTeamId = match.AwayTeamId;
                entity.AwayCurrentGoals = match.AwayCurrentGoals;
                entity.HomeCurrentGoals = match.HomeCurrentGoals;
                entity.AwayFulltimeGoals = match.AwayFulltimeGoals;
                entity.HomeFulltimeGoals = match.HomeFulltimeGoals;
                entity.AwayPenaltyGoals = match.AwayPenaltyGoals;
                entity.HomePenaltyGoals = match.HomePenaltyGoals;
                entity.MatchStatus = match.MatchStatus;
                entity.Season = match.Season;
                entity.LeagueId = match.LeagueId;
                entity.Round = match.Round;
                entity.Date = match.Date;
                _context.MatchEntities.Update(entity);
                updated++;
            }
        }

        await _context.SaveChangesAsync();
        return (Updated: updated, Created: created);
    }

    public async Task UpdateMatchAsync(MatchEntity match)
    {
        var entity = await _context.MatchEntities.FindAsync(match.Id);
        if (entity == null)
            throw new InvalidOperationException("Match not found");

        entity.AwayCurrentGoals = match.AwayCurrentGoals;
        entity.HomeCurrentGoals = match.HomeCurrentGoals;
        entity.AwayFulltimeGoals = match.AwayFulltimeGoals;
        entity.HomeFulltimeGoals = match.HomeFulltimeGoals;
        entity.AwayPenaltyGoals = match.AwayPenaltyGoals;
        entity.HomePenaltyGoals = match.HomePenaltyGoals;
        entity.MatchStatus = match.MatchStatus;
        entity.Date = match.Date;
        _context.MatchEntities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<MatchEntity> GetAllMatches(int leagueId)
    {
        var matches = _context.MatchEntities.Where(m => m.LeagueId == leagueId).Include("HomeTeam").Include("AwayTeam")
            .ToList();
        return matches;
    }

    public async Task<IEnumerable<MatchEntity>> GetNotStartedMatches(int leagueId, TimeSpan timeSpan)
    {
        var latestMatchDate = DateTimeHelper.GetNow().Add(timeSpan);
        var matches = await _context.MatchEntities
            .Where(m => m.LeagueId == leagueId && m.Date < latestMatchDate && m.MatchStatus == MatchStatus.NS)
            .ToListAsync();
        return matches;
    }

    public async Task<LeagueEntity> GetLeague(int leagueId)
        => await _context.LeagueEntities.FirstOrDefaultAsync(l => l.Id == leagueId) ?? throw new Exception();

    public async Task<IEnumerable<MatchEntity>> GetMatchesAsync(int leagueId, TimeSpan timeSpan)
    {
        var dateTimeNow = DateTimeHelper.GetNow();
        var date = DateTimeHelper.GetNow().Add(-timeSpan);
        var matches = await _context.MatchEntities
            .Where(m => m.LeagueId == leagueId
                        && m.Date < dateTimeNow && m.Date > date
                        && ((int)m.MatchStatus <= (int)MatchStatus.P || m.MatchStatus == MatchStatus.LIVE || m.MatchStatus == MatchStatus.BT))
            .ToListAsync();
        return matches;
    }
}