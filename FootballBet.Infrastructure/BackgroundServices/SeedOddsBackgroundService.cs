using FootballBet.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.BackgroundServices;

public class SeedOddsBackgroundService: BackgroundService
{
    private readonly ILogger<SeedOddsBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SeedOddsBackgroundService(ILogger<SeedOddsBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var hour12Task =
            Task.Run(async () => { await RunAsync(TimeSpan.FromHours(12), TimeSpan.FromDays(14), stoppingToken); },
                stoppingToken);

        var hourTask =
            Task.Run(async () => { await RunAsync(TimeSpan.FromHours(1), TimeSpan.FromHours(12), stoppingToken); },
                stoppingToken);
        var minuteTask =
            Task.Run(async () => { await RunAsync(TimeSpan.FromMinutes(1), TimeSpan.FromHours(1), stoppingToken); },
                stoppingToken);
        await Task.WhenAll(hour12Task, hourTask, minuteTask);
    }

    private async Task RunAsync(TimeSpan taskTimeSpan, TimeSpan matchesTimeSpan, CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("seeding odds for matches starting the next week");
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IOddsService>();
                await service.SaveOddsAsync(1, "2022", matchesTimeSpan);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error seeding odds");
            }

            await Task.Delay(taskTimeSpan, stoppingToken);
        }
    }
}