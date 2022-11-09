using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Shared.Models.Bets;

namespace FootballBet.Infrastructure.Services;

public class BetService : IBetService
{
    private readonly IBetRepository _betRepository;
    private readonly IOddsRepository _oddsRepository;

    public BetService(IBetRepository betRepository, IOddsRepository oddsRepository)
    {
        _betRepository = betRepository;
        _oddsRepository = oddsRepository;
    }

    public async Task<BetEntity> PlaceBetAsync(int oddsId, int matchId, string userId, decimal amount, string groupId)
        => await _betRepository.PlaceBetAsync(new BetEntity
        {
            OddsId = oddsId,
            UserId = userId,
            WagerAmount = amount,
            BettingGroupId = groupId,
            MatchId = matchId
        });

    public async Task<IEnumerable<BetRequest>> GetBets(string userId, string groupId)
    {
        var bets = await _betRepository.GetBetsAsync(userId, groupId);
        return new List<BetRequest>();
    }

    public async Task<BetResponse?> GetBet(string userId, int matchId, string groupId)

    {
        var bet = await _betRepository.GetBetAsync(userId, matchId, groupId);
        if (bet is null)
            throw new InvalidOperationException("Bet not found");
        var odds = await _oddsRepository.GetOddsAsync(bet.OddsId);
        if (odds is null)
            throw new InvalidOperationException("Odds not found");
        var baseOdds = await _oddsRepository.GetBaseOddsAsync(bet.OddsId, odds.MatchWinnerEntityEnum);
        if(baseOdds is null)
            throw new InvalidOperationException("Base odds not found");
        return bet.ToBetDto(odds, baseOdds);
    }
}