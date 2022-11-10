using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballBet.Repository.Repositories;

public class BetRepository : IBetRepository
{
    private readonly ApplicationDbContext _context;

    public BetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BetEntity> GetBetByIdAsync(int id)
        => await _context.BetEntities.FindAsync(id) ?? throw new InvalidOperationException("Bet not found");

    public async Task<IEnumerable<BetEntity>> GetBetsAsync(string userId, string bettingGroupId)
        => await _context.BetEntities.Where(b => b.UserId == userId && b.BettingGroupId == bettingGroupId)
            .ToListAsync();

    public async Task<IEnumerable<BetEntity>> GetBetsAsync(int matchId, string bettingGroupId)
        => throw new NotImplementedException();

    public async Task<BetEntity?> GetBetAsync(string userId, int matchId, string groupId)
    {
        var bet = await _context.BetEntities.SingleOrDefaultAsync(
            b => b.UserId == userId && b.BettingGroupId == groupId && b.MatchId == matchId);
        return bet;
    }

    public async Task<BetEntity> PlaceBetAsync(BetEntity bet)
    {
        //check if bet for user and match already exists
        var existingBet = await _context.BetEntities.SingleOrDefaultAsync(b =>
            b.UserId == bet.UserId && b.MatchId == bet.MatchId && b.BettingGroupId == bet.BettingGroupId);
        if (existingBet is not null)
            throw new InvalidOperationException("Already placed bet on match");
        var match = await _context.MatchEntities.FindAsync(bet.MatchId);
        if (match is null)
            throw new InvalidOperationException("Match not found");
        if (match.MatchStatus != MatchStatus.NS)
            throw new InvalidOperationException("Can only place bets on matches that are not started yet");
        await _context.BetEntities.AddAsync(bet);
        await _context.SaveChangesAsync();
        return bet;
    }

    public async Task<IEnumerable<BetEntity>> GetUnprocessedBetsAsync(int matchId)
        => await _context.BetEntities.Where(b => b.MatchId == matchId && b.Processed == false).ToListAsync();

    public async Task ProcessExactWinAsync(int betId)
    {
        var bet = await _context.BetEntities.FindAsync(betId);
        if (bet is null)
            throw new InvalidOperationException("Bet not found");
        var oddsForBet = await _context.ExactScoreOddsEntities.FindAsync(bet.OddsId);
        if (oddsForBet is null)
            throw new InvalidOperationException("Odds not found");
        var payoutAmount = bet.WagerAmount * oddsForBet.Odds;
        var userBalance = await _context.UserBalanceEntities.SingleOrDefaultAsync(ub => ub.UserId == bet.UserId);
        if (userBalance is null)
            throw new InvalidOperationException("User balance not found");
        userBalance.Balance+= payoutAmount;
        bet.Processed = true;
        bet.IsWinningBet = true;
        await _context.SaveChangesAsync();  
    }

    public async Task ProcessBaseWinAsync(int betId)
    {
        var bet = await _context.BetEntities.FindAsync(betId);
        if (bet is null)    
            throw new InvalidOperationException("Bet not found");
        var exactScoreOdds = await _context.ExactScoreOddsEntities.FindAsync(bet.OddsId);
        if (exactScoreOdds is null)
            throw new InvalidOperationException("Odds not found");
        var baseOdds = await _context.BaseOddsEntities.SingleOrDefaultAsync(b =>
            b.MatchOddsGroupId == exactScoreOdds.MatchOddsGroupId &&
            b.MatchWinnerEntityEnum == exactScoreOdds.MatchWinnerEntityEnum);
        if (baseOdds is null){
            throw new InvalidOperationException("Base odds not found");
        }
        var payoutAmount = bet.WagerAmount * baseOdds.Odds;
        var userBalance = await _context.UserBalanceEntities.SingleOrDefaultAsync(ub => ub.UserId == bet.UserId);
        if (userBalance is null)
            throw new InvalidOperationException("User balance not found");
        userBalance.Balance += payoutAmount;
        bet.Processed = true;
        bet.IsWinningBet = true;
        await _context.SaveChangesAsync();
    }

    public async Task ProcessLossAsync(int betId)
    {
        var bet = await _context.BetEntities.FindAsync(betId);
        if (bet is null)
            throw new InvalidOperationException("Bet not found");
        bet.Processed = true;
        bet.IsWinningBet = false;
        await _context.SaveChangesAsync();
    }
}