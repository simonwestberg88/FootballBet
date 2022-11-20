using FootballBet.Infrastructure.Services;
using FootballBet.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.BackgroundServices;

public class LiveMatchesBackgroundService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<LiveMatchesBackgroundService> _logger;

    public LiveMatchesBackgroundService(IServiceProvider serviceProvider, ILogger<LiveMatchesBackgroundService> logger )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Run(async () =>
            {
                _logger.LogInformation("LiveMatchesBackgroundService is working. TimeNow {DateTime}", DateTimeHelper.GetNow().ToString("hh:mm:ss"));
                var scope = _serviceProvider.CreateScope();
                var matchService = scope.ServiceProvider.GetService<IMatchService>();
                await matchService!.UpdateLiveMatchesAsync();
            }, stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}