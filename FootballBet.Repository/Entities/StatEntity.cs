namespace FootballBet.Repository.Entities;

public class StatEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string GroupId { get; set; }
    public int ExactWins { get; set; }
    public int BaseWins { get; set; }
    public int Losses { get; set; }
}