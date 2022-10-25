using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Home
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("winner")]
        public bool? Winner { get; set; }
    }
}
