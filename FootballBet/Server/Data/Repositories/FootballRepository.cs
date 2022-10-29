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
            await _context.LeagueEntities.AddAsync(league);
            await _context.SaveChangesAsync();
            return league;
        }

        public async Task<IEnumerable<TeamEntity>> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams)
        {
            await _context.TeamEntities.AddRangeAsync(teams);
            await _context.SaveChangesAsync();
            return teams;
        }

        public async Task<IEnumerable<MatchEntity>> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches)
        {
            await _context.MatchEntities.AddRangeAsync(matches);
            await _context.SaveChangesAsync();
            return matches;
        }
    }
}
