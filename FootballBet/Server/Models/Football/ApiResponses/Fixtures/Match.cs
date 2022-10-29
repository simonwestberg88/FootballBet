using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Match
    {
        [JsonProperty("fixture")]
        public Fixture Fixture { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("teams")]
        public Teams Teams { get; set; }

        [JsonProperty("goals")]
        public MatchScore Goals { get; set; }

        [JsonProperty("score")]
        public Score Score { get; set; }
    }
}
