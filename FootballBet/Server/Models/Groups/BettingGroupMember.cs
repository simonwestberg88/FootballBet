using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Server.Models.Groups
{
    public class BettingGroupMember
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string UserId { get; set; }
        public Guid BettingGroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
