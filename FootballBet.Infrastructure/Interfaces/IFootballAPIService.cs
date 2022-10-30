namespace FootballBet.Infrastructure.Interfaces;

public interface IFootballAPIService
{
    public Task<string> GetWorldCup();
    public Task<string> SeedDatabase();
}