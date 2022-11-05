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

    public async Task<BetEntity> PlaceBetAsync(int oddsId, string userId, decimal amount, string groupId)
        => await _betRepository.PlaceBetAsync(new BetEntity
        {
            OddsId = oddsId,
            UserId = userId,
            WagerAmount = amount,
            BettingGroupId = groupId
        });

    public async Task<IEnumerable<BetEntity>> GetBetsForUserAsync(string userId, string groupId)
        => await _betRepository.GetBetsByUserIdAsync(userId, groupId);
}