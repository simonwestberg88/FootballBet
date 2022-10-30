namespace FootballBet.Server.Data.Services.Interfaces
{
    public interface IFootballAPIService
    {
        public Task<string> GetWorldCup();
        public Task<string> SeedDatabase();
    }
}
