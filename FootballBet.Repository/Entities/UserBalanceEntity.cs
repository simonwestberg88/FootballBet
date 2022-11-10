namespace FootballBet.Repository.Entities;

public class UserBalanceEntity
{
    public string UserId { get; set; }
    public decimal Balance { get; set; }
    public string GroupId { get; set; }
}