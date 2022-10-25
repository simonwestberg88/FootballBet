using FootballBet.Server.Models.Football.Common;
using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Away
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
