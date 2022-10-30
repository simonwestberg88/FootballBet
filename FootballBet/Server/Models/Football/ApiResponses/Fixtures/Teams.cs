using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Teams
    {
        [JsonProperty("home")]
        public Team Home { get; set; }

        [JsonProperty("away")]
        public Team Away { get; set; }
    }
}
