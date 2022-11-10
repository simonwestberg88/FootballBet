using FootballBet.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballBet.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupStatsController : Controller
    {
        private readonly IStatsService _statsService;

        public GroupStatsController(IStatsService statsService)
            => _statsService = statsService;

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupStats([FromRoute] string groupId)
            => Ok(await _statsService.GetStatsForGroupAsync(groupId));
        
    }
}
