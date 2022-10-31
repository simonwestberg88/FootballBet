using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Fixtures;

public class Venue
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }
}