using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class MatchScore
    {
        [JsonPropertyName("home")]
        public int? Home { get; set; }

        [JsonPropertyName("away")]
        public int? Away { get; set; }
    }
}
