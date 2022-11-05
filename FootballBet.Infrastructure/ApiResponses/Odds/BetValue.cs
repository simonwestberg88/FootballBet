using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct BetValue(
    [property: JsonPropertyName("value")] object Prediction,
    [property: JsonPropertyName("odd")] string Odd
);