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
        //wait 5 min before starting
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("seeding odds for matches starting within the next 7 days");
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IOddsService>();
                await service.SaveOddsAsync(1, "2022", TimeSpan.FromDays(7));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error seeding odds");
            }

            await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
        }
    }
}