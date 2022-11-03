// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct Bet(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("values")] IReadOnlyList<Value> Values
);

public record struct Bookmaker(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("bets")] IReadOnlyList<Bet> Bets
);

public record struct Match(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("timezone")]
    string Timezone,
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("timestamp")]
    int Timestamp
);

public record struct League(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("country")]
    string Country,
    [property: JsonPropertyName("logo")] string Logo,
    [property: JsonPropertyName("flag")] object Flag,
    [property: JsonPropertyName("season")] int Season
);

public record struct Paging(
    [property: JsonPropertyName("current")]
    int Current,
    [property: JsonPropertyName("total")] int Total
);

public record struct Parameters(
    [property: JsonPropertyName("league")] string League,
    [property: JsonPropertyName("bookmaker")]
    string Bookmaker,
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("season")] string Season
);

public record struct Response(
    [property: JsonPropertyName("league")] League League,
    [property: JsonPropertyName("fixture")]
    Match Match,
    [property: JsonPropertyName("update")] DateTime Update,
    [property: JsonPropertyName("bookmakers")]
    IReadOnlyList<Bookmaker> Bookmakers
);

public record struct OddsResponse(
    [property: JsonPropertyName("get")] string Get,
    [property: JsonPropertyName("parameters")]
    Parameters Parameters,
    [property: JsonPropertyName("errors")] IReadOnlyList<object> Errors,
    [property: JsonPropertyName("results")]
    int Results,
    [property: JsonPropertyName("paging")] Paging Paging,
    [property: JsonPropertyName("response")]
    IReadOnlyList<Response> Response
);

public record struct Value(
    [property: JsonPropertyName("value")] string Prediction,
    [property: JsonPropertyName("odd")] string Odd
);