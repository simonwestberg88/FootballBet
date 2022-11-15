using FootballBet.Shared.Models.Users;

namespace FootballBet.Shared.Models.Stats
{
    public class MemberGameDayStatsShared
    {
        public UserDto User { get; set; }
        public decimal TotalWinningsForDay { get; set; }
    }
}
