using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct Match(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("timezone")]
    string Timezone,
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("timestamp")]
    int Timestamp
);