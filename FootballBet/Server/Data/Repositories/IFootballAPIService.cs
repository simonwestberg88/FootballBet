namespace FootballBet.Server.Data.Repositories
{
    public interface IFootballAPIService
    {
        public Task<string> GetWorldCup();
        public Task<string> SeedDatabase();
    }
}
