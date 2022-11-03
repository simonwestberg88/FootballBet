namespace FootballBet.Client.Services;
public interface IBalanceService
{
    Task<decimal> GetBalanceAsync();
    Task<decimal> DepositAsync(decimal amount);
    Task<decimal> WithdrawAsync(decimal amount);
}
public class BalanceService
{
    
}