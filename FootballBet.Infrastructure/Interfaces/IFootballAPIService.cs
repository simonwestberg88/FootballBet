using FootballBet.Shared.Models.Match;

namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballAPIService
{
    public Task<string> GetWorldCup();
    public Task<string> SeedDatabase(string? year, int? leagueId);
    public IEnumerable<MatchDto> GetMatches(int leagueId);
}