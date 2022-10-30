using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Repository.Entities;

public class BettingGroupInvitationEntity
{
    [Key]
    public Guid BettingGroupInvitationId { get; set; }
    public Guid BettingGroupId { get; set; }
    public string InvitedUserEmail { get; set; }
    public string InvitingUserId { get; set; }
    [ForeignKey("InvitingUserId")]
    public ApplicationUser InvitingUser { get; set; }
    [ForeignKey("BettingGroupId")]
    public BettingGroupEntity BettingGroupEntity { get; set; }
}