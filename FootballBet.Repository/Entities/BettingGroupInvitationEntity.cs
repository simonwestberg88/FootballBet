using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Repository.Entities;

public class BettingGroupInvitationEntity
{
    [Key]
    public Guid BettingGroupInvitationId { get; set; }
    public Guid BettingGroupEntityId { get; set; }
    public string InvitedUserEmail { get; set; }
    public string InvitingUserId { get; set; }
    [ForeignKey("InvitingUserId")]
    public ApplicationUser InvitingUser { get; set; }
    [ForeignKey("BettingGroupEntityId")]
    public BettingGroupEntity BettingGroupEntity { get; set; }
}