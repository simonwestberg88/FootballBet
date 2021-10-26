using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Server.Models.Groups
{
    public class BettingGroupInvitation
    {
        public Guid GroupId { get; set; }
        public Guid BettingGroupId { get; set; }
        [ForeignKey("BettingGroupId")]
        public BettingGroup BettingGroup { get; set; }
    }
}
