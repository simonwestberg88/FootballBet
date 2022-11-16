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
    private readonly IMatchRepository _matchRepository;
    private readonly IUserRepository _userRepository;

    public BetService(IBetRepository betRepository, IOddsRepository oddsRepository,
        IMatchRepository matchRepository, IUserRepository userRepository)
    {
        _betRepository = betRepository;
        _oddsRepository = oddsRepository;
        _matchRepository = matchRepository;
        _userRepository = userRepository;
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
        if (baseOdds is null)
            throw new InvalidOperationException("Base odds not found");
        return bet.ToBetDto(odds, baseOdds);
    }

    public async Task<List<GroupVisibleBetDto>> GetBetsForGameAsync(int matchId, string groupId)
    {
        var match = await _matchRepository.GetMatchAsync(matchId);
        if (!MatchIsStartedOrFinished(match.MatchStatus))
            throw new InvalidOperationException("Match has not started yet");

        var bets = await _betRepository.GetBetsForGroupAndGameAsync(matchId, groupId);
        var groupBetList = new List<GroupVisibleBetDto>();
        foreach (var bet in bets)
        {
            var odds = await _oddsRepository.GetOddsAsync(bet.OddsId);
            if (odds is null)
                throw new InvalidOperationException("Odds not found");
            var baseOdds = await _oddsRepository.GetBaseOddsAsync(bet.OddsId, odds.MatchWinnerEntityEnum);
            if (baseOdds is null)
                throw new InvalidOperationException("Base odds not found");
            var nickName = await _userRepository.GetBettingGroupUserNicknameByUserIdAsync(bet.UserId);
            groupBetList.Add(bet.ToGroupVisibleBetDto(odds, baseOdds, nickName));
        }
        return groupBetList.OrderBy(x => x.Nickname).ToList();
    }

    private bool MatchIsStartedOrFinished(MatchStatus status)
        => status switch
        {
            MatchStatus.TBD => false,
            MatchStatus.NS => false,
            _ => true
        };
}