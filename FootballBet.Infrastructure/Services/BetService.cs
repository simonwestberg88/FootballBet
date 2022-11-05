using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Services;

public class BetService : IBetService
{
    private readonly IBetRepository _betRepository;

    public BetService(IBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    public async Task PlaceBetAsync(int oddsId, string userId, decimal amount, string groupId)
    {
        var bet = new BetEntity
        {
            OddsId = oddsId,
            UserId = userId,
            WagerAmount = amount,
            BettingGroupId = groupId
        };
        await _betRepository.PlaceBetAsync(bet);
    }

    public async Task<IEnumerable<BetEntity>> GetBetsForUserAsync(string userId, string groupId)
        => await _betRepository.GetBetsByUserIdAsync(userId, groupId);
}