using FootballBet.Shared.Models.Stats;

namespace FootballBet.Infrastructure.Interfaces
{
    public interface IStatsService
    {
        public Task<GameDayStatsContainerShared> GetStatsForGroupAsync(string groupId);
        public Task<BetStatsDto> GetAppBarStatsAsync(string groupId, string userId);
        public Task<WinStats> GetWinStatsAsync(string groupId);
        public Task<WinStats> GetWinStatsAsync(string groupId, string userId);
        public Task<WinStatsResponse> GetTop3WinStatsAsync(string groupId);
    }
}
