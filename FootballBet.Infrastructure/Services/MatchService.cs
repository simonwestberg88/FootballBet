using FootballBet.Infrastructure.ApiResponses.Fixtures;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Mappers;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Shared.Models.Match;
using Microsoft.Extensions.Logging;

namespace FootballBet.Infrastructure.Services;

public interface IMatchService
{
    public Task<string> SeedMatchesAsync(int leagueId, string season);
    public IEnumerable<MatchDto> GetMatches(int leagueId);
    public Task UpdateLiveMatchesAsync();

}
public class MatchService: IMatchService
{
    private readonly IFootballApiClient _footballApiClient;
    private readonly IFootballRepository _footballRepository;
    private readonly ILogger<MatchService> _logger;

    public MatchService(IFootballApiClient footballApiClient, IFootballRepository footballRepository, ILogger<MatchService> logger)
    {
        _footballApiClient = footballApiClient;
        _footballRepository = footballRepository;
        _logger = logger;
    }

    public async Task<string> SeedMatchesAsync(int leagueId, string season)
    {
        var matches = await _footballApiClient.GetFixtures(leagueId, season);
        await _footballRepository.CreateOrUpdateLeague(matches.First().League.ToLeagueEntity());
        var teams = new List<Team>();
        teams.AddRange(matches.Select(m => m.Teams.Home).ToList());
        teams.AddRange(matches.Select(m => m.Teams.Away).ToList());
        teams = teams.DistinctBy(t => t.Id).ToList();
        var teamResult = await _footballRepository.CreateOrUpdateTeams(teams.Select(t => t.ToTeamEntity()));
        var fixtures = await _footballRepository.CreateOrUpdateMatches(matches.Select(m => m.ToMatchEntity()));


        return $"Amount of teams added: {teamResult.Created}. Amount of teams updated: {teamResult.Updated}. " +
               $"Amount of fixtures added: {fixtures.Created}. Amount of fixtures updated: {fixtures.Created}";
    }
    
    public IEnumerable<MatchDto> GetMatches(int leagueId)
        => _footballRepository.GetAllMatches(leagueId).Select(e => e.ToMatchDto());

    public async Task UpdateLiveMatchesAsync()
    {
        var liveMatches = (await _footballRepository.GetMatchesAsync(1, TimeSpan.FromHours(4))).ToList();
        _logger.LogInformation("Live matches: {Matches}",  string.Join(", ", liveMatches.Select(m => m.Id).ToList()));
        foreach (var match in liveMatches)
        {
            var liveMatch = await _footballApiClient.GetMatch(match.Id);
            if (liveMatch != null)
            {
                await _footballRepository.UpdateMatchAsync(liveMatch.ToMatchEntity());
            }
        }
    }
}