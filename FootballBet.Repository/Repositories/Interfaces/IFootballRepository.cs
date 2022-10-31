using FootballBet.Repository.Entities;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IFootballRepository
    {
        Task<LeagueEntity> CreateOrUpdateLeague(LeagueEntity league);
        Task<(int Updated, int Created)> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams);
        Task<(int Updated, int Created)> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches);
        IEnumerable<MatchEntity> GetAllMatchesForLeagueId(int leagueId);
    }
}
