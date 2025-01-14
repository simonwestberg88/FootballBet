﻿using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces
{
    public interface IFootballRepository
    {
        Task<LeagueEntity> CreateOrUpdateLeague(LeagueEntity league);
        Task<(int Updated, int Created)> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams);
        Task<(int Updated, int Created)> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches);
        Task UpdateMatchAsync(MatchEntity match);
        IEnumerable<MatchEntity> GetAllMatches(int leagueId);
        Task<IEnumerable<MatchEntity>> GetNotStartedMatches(int leagueId, TimeSpan timeSpan);
        Task<LeagueEntity> GetLeague(int leagueId);
        Task<IEnumerable<MatchEntity>> GetMatchesAsync(int leagueId, TimeSpan timeSpan);
    }
}
