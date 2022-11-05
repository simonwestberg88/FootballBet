using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct Parameters(
    [property: JsonPropertyName("league")] string League,
    [property: JsonPropertyName("bookmaker")]
    string Bookmaker,
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("season")] string Season
);