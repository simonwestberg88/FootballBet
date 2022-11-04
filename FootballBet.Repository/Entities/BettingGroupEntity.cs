namespace FootballBet.Repository.Entities
{
    public class BettingGroupEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<BettingGroupMemberEntity> Memberships { get; set; }
        public DateTime CreatedAt { get; set; }
        //TODO:needs a connection to competitions in the future
    }

   
}
