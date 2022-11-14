using FootballBet.Shared.Models.Match;

namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballApiService
{
    public Task<string> SeedDatabase(string? year, int? leagueId);
    public IEnumerable<MatchDto> GetMatches(int leagueId);
    Task<string> GetWorldCup();
}