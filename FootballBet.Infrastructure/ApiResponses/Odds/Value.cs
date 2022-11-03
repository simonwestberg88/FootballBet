using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct Value(
    [property: JsonPropertyName("value")] string Prediction,
    [property: JsonPropertyName("odd")] string Odd
);