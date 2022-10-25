using FootballBet.Server.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballBet.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FootballLeagueController : ControllerBase
    {
        private readonly IFootballAPIService _footballApiService;

        public FootballLeagueController(IFootballAPIService footballApiService)
            => _footballApiService = footballApiService;

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            return Ok(await _footballApiService.GetLeagues());
        }
    }
}
