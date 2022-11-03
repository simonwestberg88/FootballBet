using FootballBet.Infrastructure.Interfaces;
using FootballBet.Repository.Repositories.Interfaces;

namespace FootballBet.Infrastructure.Services;

public class TransactionService: ITransactionService
{
    private readonly IUserRepository _userRepository;
    public TransactionService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<decimal> GetBalanceAsync(string userId, CancellationToken cancellationToken = default)
     => await _userRepository.GetBalanceAsync(userId, cancellationToken);

    public async Task<decimal> WithdrawAsync(string userId, decimal amount, CancellationToken token = default)
        => await _userRepository.WithdrawAsync(userId, amount, token);
    
    public async Task<decimal> DepositAsync(string userId, decimal amount, CancellationToken token = default)
        => await _userRepository.DepositAsync(userId, amount, token);
}
