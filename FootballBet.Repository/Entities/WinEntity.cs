namespace FootballBet.Repository.Entities;

public class WinEntity
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime WinDate { get; set; }
}