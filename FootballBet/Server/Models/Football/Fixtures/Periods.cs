using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Periods
    {
        [JsonPropertyName("first")]
        public int? First { get; set; }

        [JsonPropertyName("second")]
        public int? Second { get; set; }
    }
}
