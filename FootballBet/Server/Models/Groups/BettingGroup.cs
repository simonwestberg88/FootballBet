namespace FootballBet.Server.Models.Groups
{
    public class BettingGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<BettingGroupMember> Memberships { get; set; }
    }
}
