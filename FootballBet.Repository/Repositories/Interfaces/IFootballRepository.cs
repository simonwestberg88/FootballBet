﻿using FootballBet.Repository.Entities;

namespace FootballBet.Server.Data.Repositories.Interfaces
{
    public interface IFootballRepository
    {
        Task<LeagueEntity> CreateOrUpdateLeague(LeagueEntity league);
        Task<IEnumerable<TeamEntity>> CreateOrUpdateTeams(IEnumerable<TeamEntity> teams);
        Task<IEnumerable<MatchEntity>> CreateOrUpdateMatches(IEnumerable<MatchEntity> matches);
        IEnumerable<MatchEntity> GetAllMatchesForLeagueId(int leagueId);
    }
}
