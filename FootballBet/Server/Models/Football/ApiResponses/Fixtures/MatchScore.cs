using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class MatchScore
    {
        [JsonProperty("home")]
        public int? Home { get; set; }

        [JsonProperty("away")]
        public int? Away { get; set; }
    }
}
