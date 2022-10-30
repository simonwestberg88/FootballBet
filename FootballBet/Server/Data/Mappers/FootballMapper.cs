using FootballBet.Server.Models.Football.ApiResponses.Fixtures;
using FootballBet.Server.Models.Football.DBModels;

namespace FootballBet.Server.Data.Mappers
{
    public static class FootballMapper
    {
        public static TeamEntity ToTeamEntity(this Team team)
           => new TeamEntity()
           {
               Id = team.Id,
               LogoUrl = team.Logo,
               Name = team.Name
           };

        public static LeagueEntity ToLeagueEntity(this League league)
            => new LeagueEntity()
            {
                Id = league.Id,
                LogoUrl = league.Logo,
                Name = league.Name,
                Season = league.Season
            };


        public static MatchEntity ToMatchEntity(this Match match)
            => new MatchEntity()
            {
                AwayCurrentGoals = match.Goals.Away,
                HomeCurrentGoals = match.Goals.Home,
                AwayFulltimeGoals = match.Score.Fulltime.Away,
                HomeFulltimeGoals = match.Score.Fulltime.Home,
                AwayPenaltyGoals = match.Score.Penalty.Away,
                HomePenaltyGoals = match.Score.Penalty.Home,
                AwayTeamId = match.Teams.Away.Id,
                HomeTeamId = match.Teams.Home.Id,
                Id = match.Fixture.Id,
                Date = match.Fixture.Date,
                LeagueId = match.League.Id,
                MatchStatus = ToMatchStatus(match.Fixture.Status.Short),
                Round = match.League.Round,
                Season = match.League.Season
            };

        private static MatchStatus ToMatchStatus(string status)
        {
            if (status == "1H")
                return MatchStatus.FH;
            if (status == "2H")
                return MatchStatus.SH;
            return Enum.Parse<MatchStatus>(status);
        }
    }
}
