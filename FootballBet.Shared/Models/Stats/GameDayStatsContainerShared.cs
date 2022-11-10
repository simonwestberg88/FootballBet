using FootballBet.Shared.Models.Users;

namespace FootballBet.Shared.Models.Stats
{
    public class GameDayStatsContainerShared
    {
        public List<GameDayStatsShared> GameDayStats { get; set; }
        public List<DateTime> GameDates { get; set; }
        public List<UserDto> Members { get; set; }
    }
}
