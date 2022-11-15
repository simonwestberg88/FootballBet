using Duende.IdentityServer.Extensions;
using FootballBet.Infrastructure.ApiResponses.Odds;
using FootballBet.Infrastructure.Constants;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Enums;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Shared.Models.Odds;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.Services;

public interface IOddsService
{
    Task<BaseOddsResponse> GetLatestBaseOddsAsync(int matchId);
    Task<IEnumerable<ExactScoreOddsDto>> GetLatestExactOddsAsync(int matchId);
    Task SaveOddsAsync(int leagueId, string season, TimeSpan timeSpan);
}

public class OddsService : IOddsService
{
    private readonly IFootballApiClient _footballApiClient;
    private readonly ILogger<OddsService> _logger;
    private readonly IOddsRepository _oddsRepository;
    private readonly IFootballRepository _footballRepository;

    public OddsService(IFootballApiClient footballApiClient, ILogger<OddsService> logger,
        IOddsRepository oddsRepository, IFootballRepository footballRepository)
    {
        _footballApiClient = footballApiClient;
        _logger = logger;
        _oddsRepository = oddsRepository;
        _footballRepository = footballRepository;
    }

    public async Task<BaseOddsResponse> GetLatestBaseOddsAsync(int matchId)
    {
        var baseOdds = (await _oddsRepository.GetLatestBaseOddsAsync(matchId)).ToList();
        if (baseOdds.Count != 0)
            return new BaseOddsResponse
            {
                Away = baseOdds.Single(b => b.MatchWinnerEntityEnum == MatchWinnerEntityEnum.Away).ToBaseOddsDto(),
                Draw = baseOdds.Single(b => b.MatchWinnerEntityEnum == MatchWinnerEntityEnum.Draw).ToBaseOddsDto(),
                Home = baseOdds.Single(b => b.MatchWinnerEntityEnum == MatchWinnerEntityEnum.Home).ToBaseOddsDto()
            };
        _logger.LogWarning("No base odds found for match with id {MatchId}", matchId);
        return new BaseOddsResponse();
    }


    public async Task<IEnumerable<ExactScoreOddsDto>> GetLatestExactOddsAsync(int matchId)
    {
        var odds = (await _oddsRepository.GetLatestExactScoreOddsAsync(matchId)).ToList();
        if (odds.Any()) return odds.Select(o => o.ToOddsDto());
        _logger.LogInformation("no exact odds available");
        return new List<ExactScoreOddsDto>();
    }

    public async Task SaveOddsAsync(int leagueId, string season, TimeSpan timeSpan)
    {
        var matches = await _footballRepository.GetNotStartedMatches(leagueId, timeSpan);
        var tasks = matches.Select(m => _footballApiClient.GetOddsAsync(m.Id));
        var bet365Odds = await Task.WhenAll(tasks);
        foreach (var odds in bet365Odds)
        {
            if (odds.HasValue)
            {
                await AddOddsForMatchAsync(odds.Value);
            }
        }
    }

    private static (IEnumerable<ExactScoreOddsEntity>, IEnumerable<BaseOddsEntity>) CreateOddsEntities(
        BookmakerOdds bookmakerOdds, int matchOddsGroupId)
    {
        var exactScore = bookmakerOdds.Bets
            .Where(b => b.Name == "Exact Score").SelectMany(b => b.Values).ToList();
        var baseOdds = bookmakerOdds.Bets
            .Where(b => b.Name == "Match Winner").SelectMany(b => b.Values).ToList();
        var exactScoreEntity = exactScore.Select(w => w.ToOddsEntity(matchOddsGroupId)).ToList();
        var baseOddsEntity = baseOdds.Select(w => w.ToBaseOddsEntity(matchOddsGroupId)).ToList();
        return (exactScoreEntity, baseOddsEntity);
    }

    private async Task AddOddsForMatchAsync(OddsResponse match)
    {
        if(match.Bookmakers.IsNullOrEmpty()) return;
        var bookmakerOddsForMatch = match
            .Bookmakers.FirstOrDefault(b => b.Id == ApiConstants.Bet365Id);
        if (bookmakerOddsForMatch.Bets.IsNullOrEmpty()) return;

        var oddsGroupId = await _oddsRepository.AddOddsGroupAsync(new MatchOddsGroupEntity
            { MatchId = match.Match.Id, Created = DateTime.Now });

        var (exactScoreOdds, baseOdds) = CreateOddsEntities(bookmakerOddsForMatch, oddsGroupId);
        await _oddsRepository.AddOddsAsync(exactScoreOdds);
        await _oddsRepository.AddBaseOddsAsync(baseOdds);
        _logger.LogInformation("Saved odds for match {MatchId}", match.Match.Id);
    }
}