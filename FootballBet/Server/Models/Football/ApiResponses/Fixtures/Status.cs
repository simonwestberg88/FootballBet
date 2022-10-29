using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Status
    {
        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }

        [JsonProperty("elapsed")]
        public int? Elapsed { get; set; }
    }
}
