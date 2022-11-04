using FootballBet.Repository.Entities;

namespace FootballBet.Repository.Repositories.Interfaces;

public interface IOddsRepository
{
    public Task AddOddAsync(OddsEntity odds);
}