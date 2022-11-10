using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.Services;

public class BetPayoutBackgroundService : BackgroundService
{
    private readonly ILogger<BetPayoutBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public BetPayoutBackgroundService(ILogger<BetPayoutBackgroundService> logger, IServiceProvider serviceProvider)
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
                    _logger.LogInformation("processing bets");
                    using var scope = _serviceProvider.CreateScope();
                    var payoutService = scope.ServiceProvider.GetRequiredService<IBetPayoutService>();
                    await payoutService.ProcessBetsAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error processing bets");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }, stoppingToken);
    }
}