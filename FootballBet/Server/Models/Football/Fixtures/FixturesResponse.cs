using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class FixturesResponse
    {
        [JsonPropertyName("fixture")]
        public Fixture Fixture { get; set; }

        [JsonPropertyName("league")]
        public League League { get; set; }

        [JsonPropertyName("teams")]
        public Teams Teams { get; set; }

        [JsonPropertyName("goals")]
        public Goals Goals { get; set; }

        [JsonPropertyName("score")]
        public Score Score { get; set; }
    }
}
