using FootballBet.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballBet.Server.Controllers
{

    //[Authorize]
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

        [HttpGet("seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            return Ok(await _footballAPIService.SeedDatabase());
        }
    }
}
