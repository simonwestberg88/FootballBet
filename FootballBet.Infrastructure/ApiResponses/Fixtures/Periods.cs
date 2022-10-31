using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Fixtures;

public class Periods
{
    [JsonPropertyName("first")]
    public int? First { get; set; }

    [JsonPropertyName("second")]
    public int? Second { get; set; }
}