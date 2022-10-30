using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Repository.Entities
{
    public class BettingGroupMemberEntity
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string UserId { get; set; }
        public Guid BettingGroupEntityId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
