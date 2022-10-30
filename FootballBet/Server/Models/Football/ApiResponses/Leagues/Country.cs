using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("flag")]
        public string? Flag { get; set; }
    }
}
