using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.BackgroundServices;

public class SeedMatchesBackgroundService : BackgroundService
{
    private readonly ILogger<SeedMatchesBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SeedMatchesBackgroundService(ILogger<SeedMatchesBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("seeding matches");
                    using var scope = _serviceProvider.CreateScope();
                    var payoutService = scope.ServiceProvider.GetRequiredService<IMatchService>();
                    await payoutService.SeedMatchesAsync(1, "2022");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error seeding matches");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }, stoppingToken);
    }
}