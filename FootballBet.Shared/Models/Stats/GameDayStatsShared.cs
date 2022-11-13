namespace FootballBet.Shared.Models.Stats
{
    public class GameDayStatsShared
    {
        public DateTime Date { get; set; }
        public List<MemberGameDayStatsShared> MemberGameDayStats { get; set; } = new();

    }
}
