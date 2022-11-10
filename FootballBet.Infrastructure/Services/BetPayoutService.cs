using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Infrastructure.Services;

public interface IBetPayoutService
{
    public Task ProcessBetsAsync();
}

public class BetPayoutService : IBetPayoutService
{
    private readonly IBetRepository _betRepository;
    private readonly IOddsRepository _oddsRepository;
    private readonly IMatchRepository _matchRepository;

    public BetPayoutService(IBetRepository betRepository, IOddsRepository oddsRepository,
        IMatchRepository matchRepository)

    {
        _betRepository = betRepository;
        _oddsRepository = oddsRepository;
        _matchRepository = matchRepository;
    }

    public async Task ProcessBetsAsync()
    {
        // Get all matches that have finished
        var unprocessedMatches = (await _matchRepository.GetUnprocessedMatchesAsync()).ToList();
        // Get all bets for those matches that have not been processed
        foreach (var match in unprocessedMatches)
        {
            var bets = await _betRepository.GetUnprocessedBetsAsync(match.Id);
            var matchWinner = GetMatchWinner(match);
            foreach (var bet in bets)
            {
                var (win, exactWin) = await IsWinningBet(bet, match, matchWinner);
                if (exactWin)
                {
                    await _betRepository.ProcessExactWinAsync(bet.Id);
                }
                else if (win)
                {
                    await _betRepository.ProcessBaseWinAsync(bet.Id);
                }
                else
                {
                    await _betRepository.ProcessLossAsync(bet.Id);
                }
            }
        }
    }

    private async Task<(bool, bool)> IsWinningBet(BetEntity betEntity, MatchEntity matchEntity,
        MatchWinnerEntityEnum matchWinner)
    {
        var odds = await _oddsRepository.GetOddsAsync(betEntity.OddsId);
        if (odds is null)
            throw new Exception("Odds not found");
        var win = odds.MatchWinnerEntityEnum == matchWinner;
        var exactWin = odds.HomeTeamGoals == matchEntity.HomeFulltimeGoals && odds.AwayTeamGoals == matchEntity.AwayFulltimeGoals;
        return (win, exactWin);
    }

    private static MatchWinnerEntityEnum GetMatchWinner(MatchEntity match)
    {
        if (match.HomeFulltimeGoals > match.AwayFulltimeGoals)
        {
            return MatchWinnerEntityEnum.Home;
        }

        if (match.AwayFulltimeGoals > match.HomeFulltimeGoals)
        {
            return MatchWinnerEntityEnum.Away;
        }

        return MatchWinnerEntityEnum.Draw;
    }
}