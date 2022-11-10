using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces
{
    public interface IFootballRepository
    {
        Task<LeagueEntity> CreateOrUpdateLeague(LeagueEntity league);
        Task<(int Updated, int Created)> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams);
        Task<(int Updated, int Created)> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches);
        IEnumerable<MatchEntity> GetAllMatches(int leagueId);
        Task<IEnumerable<MatchEntity>> GetAllNotStartedMatches(int leagueId);
        Task<IEnumerable<MatchEntity>> GetMatchesStartingNextWeek(int leagueId);
        Task<IEnumerable<MatchEntity>> GetMatchesStartingNextHour(int leagueId);
        Task<LeagueEntity> GetLeague(int leagueId);
    }
}
