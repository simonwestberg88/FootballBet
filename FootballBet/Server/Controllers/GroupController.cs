using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Shared.Models.Groups;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballBet.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {

        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(BettingGroupShared group, CancellationToken ct)
            => Ok(await _groupService.CreateBettingGroup(User.Identity.GetUserId(), group.Description, group.Name, ct));

        [HttpGet]
        public async Task<IActionResult> GetGroupsForUser(CancellationToken ct)
            => Ok(await _groupService.ListGroupsForUser(User.Identity.GetUserId(), ct));

        [HttpGet("{bettingGroupId}")]
        public async Task<IActionResult> GetGroupById([FromQuery] string bettingGroupId, CancellationToken ct)
            => Ok(await _groupService.GetBettingGroupById(bettingGroupId, ct));

        [HttpPost("invitation")]
        public async Task<IActionResult> CreateInvitation(BettingGroupInvitationShared invitation, CancellationToken ct)
            => Ok(await _groupService.CreateInvitation(invitation.BettingGroupId, invitation.InvitedUserEmail, User.Identity.GetUserId(), ct));

        [HttpGet("invitation/accept/")]
        public async Task<IActionResult> ConsumeInvitation([FromQuery]string invitationId, [FromQuery]string groupId, CancellationToken ct)
        {
            await _groupService.ConsumeInvitation(invitationId, groupId, User.Identity.GetUserId(), ct);
            return NoContent();
        }

    }
}