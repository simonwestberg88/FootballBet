using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Infrastructure.Services;

public interface IBetPayoutService
{
    
    public Task PayoutBetsAsync();
}
public class BetPayoutService : IBetPayoutService
{
    private readonly IBetRepository _betRepository;
    public BetPayoutService(IBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    public Task PayoutBetsAsync()
    {
        // Get all matches that have finished
        // Get all winning bets for those matches that have not been paid out
        // Add the winnings to the user's balance
        // Update the bet to paid out
        return Task.CompletedTask;
    }
}