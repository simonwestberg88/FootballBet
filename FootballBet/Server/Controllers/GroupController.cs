using FootballBet.Server.Data.Services.Interfaces;
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

    }
}