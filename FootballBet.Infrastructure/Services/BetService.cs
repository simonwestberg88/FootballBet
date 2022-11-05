using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Infrastructure.Services;

public class BetService : IBetService
{
    private readonly IBetRepository _betRepository;

    public BetService(IBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    public async Task PlaceBetAsync(int oddsId, string userId, decimal amount, string bettingGroupId)
    {
        var bet = new BetEntity
        {
            OddsId = oddsId,
            UserId = userId,
            WagerAmount = amount,
            BettingGroupId = bettingGroupId
        };
        await _betRepository.PlaceBetAsync(bet);
    }
}