using FootballBet.Shared.Models.Bets;
using FootballBet.Shared.Models.Match;

namespace FootballBet.Client.Helpers
{
    public static class BetHelper
    {
        public static string GetBackGroundColorDependingOnStatus(MatchDto match, BetResponse userBet)
            => IsMatchFinished(match.MatchStatus)
                ? MatchHelper.GetTypeOfWin(match, userBet) switch
                {
                    "exact" => "#90F166",
                    "base" => "#EBF166",
                    _ => "#F58702"
                }
                : "#FFFFFF";

        public static bool IsMatchFinished(string matchStatus)
            => string.IsNullOrEmpty(matchStatus) ? false : matchStatus.ToLower().Contains("finish");
    }
}
