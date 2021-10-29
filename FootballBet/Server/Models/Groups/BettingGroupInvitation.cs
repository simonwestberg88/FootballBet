using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Server.Models.Groups
{
    public class BettingGroupInvitation
    {
        public Guid BettingGroupInvitationId { get; set; }
        public Guid BettingGroupId { get; set; }
        public string InvitedUserEmail { get; set; }
        public string InvitingUserId { get; set; }
        [ForeignKey("InvitingUserId")]
        public ApplicationUser InvitingUser { get; set; }
        [ForeignKey("BettingGroupId")]
        public BettingGroup BettingGroup { get; set; }
    }
}
