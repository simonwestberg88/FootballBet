using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Odds;

public record struct BookmakerOdds(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("bets")] IReadOnlyList<Bet> Bets
);