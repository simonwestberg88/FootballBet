using FootballBet.Infrastructure.Interfaces;
using FootballBet.Shared.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballBet.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FootballLeagueController : ControllerBase
    {
        private readonly IFootballAPIService _footballAPIService;

        public FootballLeagueController(IFootballAPIService footballAPIService)
        {
            _footballAPIService = footballAPIService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            return Ok(await _footballAPIService.GetWorldCup());
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedDatabase([FromBody] SeedTeamsAndFixturesShared seed)
        {
            return Ok(await _footballAPIService.SeedDatabase(seed.Year, seed.LeagueId));
        }
    }
}
