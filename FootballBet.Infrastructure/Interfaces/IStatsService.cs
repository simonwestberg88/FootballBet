using FootballBet.Shared.Models.Stats;

namespace FootballBet.Infrastructure.Interfaces
{
    public interface IStatsService
    {
        public Task<GameDayStatsContainerShared> GetStatsForGroupAsync(string groupId);
        public Task<AppBarStatsDto> GetAppBarStatsAsync(string groupId, string userId);
    }
}
