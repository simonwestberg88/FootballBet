using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Parameters
    {
        [JsonPropertyName("league")]
        public string League { get; set; }

        [JsonPropertyName("season")]
        public string Season { get; set; }
    }
}
