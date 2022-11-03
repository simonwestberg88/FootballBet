using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Infrastructure;

public class BalanceService: IBalanceService
{
    private readonly IUserRepository _userRepository;
    public BalanceService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<decimal> GetBalance(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetApplicationUserById(userId, cancellationToken);
        return user?.Balance ?? 0;
        //todo add handling if user does not exist
    }

    public async Task<decimal> UpdateBalance(string userId, decimal amount, CancellationToken cancellationToken = default)
    => await _userRepository.UpdateBalance(userId, amount, cancellationToken);
}