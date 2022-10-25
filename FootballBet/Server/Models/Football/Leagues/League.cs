using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class League
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
}
