using System.ComponentModel.DataAnnotations;

namespace FootballBet.Shared.Models.Groups
{
    public class BettingGroupShared
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Description { get; set; }
        public bool CurrentUserIsAdmin { get; set; }
        public LeagueShared? League { get; set; }
        public List<BettingGroupMemberShared>? Memberships { get; set; }
    }
}
