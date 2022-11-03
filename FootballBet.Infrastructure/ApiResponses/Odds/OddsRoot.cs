using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct OddsRoot(
    [property: JsonPropertyName("get")] string Get,
    [property: JsonPropertyName("parameters")]
    Parameters Parameters,
    [property: JsonPropertyName("errors")] IReadOnlyList<object> Errors,
    [property: JsonPropertyName("results")]
    int Results,
    [property: JsonPropertyName("paging")] Paging Paging,
    [property: JsonPropertyName("response")]
    IReadOnlyList<OddsResponse> Response
);