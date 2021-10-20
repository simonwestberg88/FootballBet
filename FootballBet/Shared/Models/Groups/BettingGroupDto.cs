using System.ComponentModel.DataAnnotations;

namespace FootballBet.Shared.Models.Groups
{
    public class BettingGroupDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public List<BettingGroupMemberDto>? Memberships { get; set; }
    }
}
