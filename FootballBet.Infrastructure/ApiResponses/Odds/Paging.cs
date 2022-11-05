using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct Paging(
    [property: JsonPropertyName("current")]
    int Current,
    [property: JsonPropertyName("total")] int Total
);