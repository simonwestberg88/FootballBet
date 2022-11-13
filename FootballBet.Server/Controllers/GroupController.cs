using FootballBet.Infrastructure.Interfaces;
using FootballBet.Shared.Models.Groups;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetGroupById(string bettingGroupId, CancellationToken ct)
            => Ok(await _groupService.GetBettingGroupById(bettingGroupId.ToString(), User.Identity.GetUserId(), ct));

        [HttpPost("invitation")]
        public async Task<IActionResult> CreateInvitation(BettingGroupInvitationShared invitation, CancellationToken ct)
        {
            await _groupService.CreateInvitation(invitation.BettingGroupId, invitation.InvitedUserEmail, User.Identity.GetUserId(), ct);
            return Ok();
        }

        [HttpPost("invitation/accept/")]
        public async Task<IActionResult> ConsumeInvitation([FromBody] BettingGroupInvitationAcceptShared acceptedInvitation, CancellationToken ct)
        {
            await _groupService.ConsumeInvitation(acceptedInvitation.InvitationId, acceptedInvitation.GroupId, User.Identity.GetUserId(), ct);
            return NoContent();
        }

        [HttpGet("changeNickname/{newNickname}/{groupId}")]
        public async Task<IActionResult> ChangeNickname([FromRoute] string newNickname, string groupId)
        {
            if (string.IsNullOrEmpty(newNickname) || newNickname.Length > 15)
                throw new InvalidDataException("Nickname is null or too long");

            await _groupService.ChangeNicknameForGroupMemberAsync(User.Identity.GetUserId(), newNickname, groupId);
            return Ok();
        }

    }
}