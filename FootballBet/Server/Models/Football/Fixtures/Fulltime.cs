using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Fulltime
    {
        [JsonPropertyName("home")]
        public int? Home { get; set; }

        [JsonPropertyName("away")]
        public int? Away { get; set; }
    }
}
