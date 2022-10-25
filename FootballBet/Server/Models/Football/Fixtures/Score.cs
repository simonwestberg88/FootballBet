using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Score
    {
        [JsonPropertyName("halftime")]
        public Halftime Halftime { get; set; }

        [JsonPropertyName("fulltime")]
        public Fulltime Fulltime { get; set; }

        [JsonPropertyName("extratime")]
        public Extratime Extratime { get; set; }

        [JsonPropertyName("penalty")]
        public Penalty Penalty { get; set; }
    }
}
