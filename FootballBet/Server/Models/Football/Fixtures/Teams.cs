using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Teams
    {
        [JsonPropertyName("home")]
        public Home Home { get; set; }

        [JsonPropertyName("away")]
        public Away Away { get; set; }
    }
}
