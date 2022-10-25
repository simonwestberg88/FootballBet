namespace FootballBet.Server.Data.Repositories
{
    public interface IFootballAPIService
    {
        public Task<string> GetLeagues();
    }
}
