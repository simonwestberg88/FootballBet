using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Models.Football.DBModels;

namespace FootballBet.Server.Data.Repositories
{
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
        public async Task<IEnumerable<TeamEntity>> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams)
        {
            foreach(var team in teams)
            {
                var entity = await _context.TeamEntities.FindAsync(team.Id);
                if(entity == null)
                    await _context.TeamEntities.AddAsync(team);
                else
                {
                    entity.LogoUrl = team.LogoUrl;
                    entity.Name = team.Name;
                    _context.TeamEntities.Update(entity);
                }
            }
            await _context.SaveChangesAsync();
            return teams;
        }

        public async Task<IEnumerable<MatchEntity>> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches)
        {
            foreach (var match in matches)
            {
                var entity = await _context.MatchEntities.FindAsync(match.Id);
                if (entity == null)
                    await _context.MatchEntities.AddAsync(match);
                else
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
                    entity.LeagueId = entity.LeagueId;
                    entity.Round = entity.Round;
                    entity.Date = entity.Date;
                    _context.MatchEntities.Update(entity);
                }
            }
            await _context.SaveChangesAsync();
            return matches;
        }

        public IEnumerable<MatchEntity> GetAllMatchesForLeagueId(int leagueId)
        {
            var matches =  _context.MatchEntities.Where(m => m.League.Id == leagueId).ToList();
            var matchesByLeague = _context.LeagueEntities.FirstOrDefault(l => l.Id == leagueId)?.Matches;
            return matches;
        }
    }
}
