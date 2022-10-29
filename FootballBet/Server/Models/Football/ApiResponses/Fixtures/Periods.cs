using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Periods
    {
        [JsonProperty("first")]
        public int? First { get; set; }

        [JsonProperty("second")]
        public int? Second { get; set; }
    }
}
