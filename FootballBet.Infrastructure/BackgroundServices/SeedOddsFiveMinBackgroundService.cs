using FootballBet.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.BackgroundServices;

public class SeedOddsFiveMinBackgroundService: BackgroundService
{
    private readonly ILogger<SeedOddsFiveMinBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SeedOddsFiveMinBackgroundService(ILogger<SeedOddsFiveMinBackgroundService> logger, IServiceProvider serviceProvider)
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
                _logger.LogInformation("seeding odds for matches starting within 5 hours");
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IOddsService>();
                await service.SaveOddsAsync(1, "2022", TimeSpan.FromHours(5));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error seeding odds");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}