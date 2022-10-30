using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Parameters
    {
        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }
    }
}
