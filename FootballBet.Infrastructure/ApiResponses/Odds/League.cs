using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct League(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("country")]
    string Country,
    [property: JsonPropertyName("logo")] string Logo,
    [property: JsonPropertyName("flag")] object Flag,
    [property: JsonPropertyName("season")] int Season
);