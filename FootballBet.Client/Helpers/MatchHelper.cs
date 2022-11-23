using FootballBet.Shared.Models.Bets;
using FootballBet.Shared.Models.Match;

namespace FootballBet.Client.Helpers
{
    public static class MatchHelper
    {
        public static string GetTypeOfWin(MatchDto match, BetResponse userBet)
        {
            if (match.AwayFulltimeGoals == userBet.AwayGoals && match.HomeFulltimeGoals == userBet.HomeGoals)
                return "exact";
            //base win
            if ((match.AwayFulltimeGoals < match.HomeFulltimeGoals && userBet.AwayGoals < userBet.HomeGoals)
                || (match.AwayFulltimeGoals > match.HomeFulltimeGoals && userBet.AwayGoals > userBet.HomeGoals)
                || (match.AwayFulltimeGoals == match.HomeFulltimeGoals && userBet.AwayGoals == userBet.HomeGoals))
                return "base";
            //lost
            return "lost";
        }
    }
}
