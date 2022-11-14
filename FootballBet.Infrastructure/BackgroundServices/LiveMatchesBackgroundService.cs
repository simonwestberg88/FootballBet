using FootballBet.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FootballBet.Infrastructure.BackgroundServices;

public class LiveMatchesBackgroundService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    public LiveMatchesBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Run(async () =>
            {
                var scope = _serviceProvider.CreateScope();
                var matchService = scope.ServiceProvider.GetService<IMatchService>();
                await matchService!.UpdateLiveMatchesAsync();
            }, stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}