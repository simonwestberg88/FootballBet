using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class League
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("flag")]
        public object Flag { get; set; } //not returned in 2018

        [JsonPropertyName("season")]
        public int Season { get; set; }

        [JsonPropertyName("round")]
        public string Round { get; set; }
    }
}
