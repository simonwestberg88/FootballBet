using System.ComponentModel.DataAnnotations.Schema;
using FootballBet.Repository.Entities;

namespace FootballBet.Server.Models.Groups
{
    public class BettingGroupMemberEntity
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string UserId { get; set; }
        public Guid BettingGroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
