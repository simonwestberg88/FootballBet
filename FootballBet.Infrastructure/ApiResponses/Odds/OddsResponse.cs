using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct OddsResponse(
    [property: JsonPropertyName("league")] League League,
    [property: JsonPropertyName("fixture")]
    Match Match,
    [property: JsonPropertyName("update")] DateTime Update,
    [property: JsonPropertyName("bookmakers")]
    IReadOnlyList<Bookmaker> Bookmakers
);