namespace FootballBet.Repository.Entities;

public class UserBalanceEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Balance { get; set; }
    public string GroupId { get; set; }
}