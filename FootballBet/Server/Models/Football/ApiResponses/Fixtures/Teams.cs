using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures;

public abstract class Teams
{
    [JsonPropertyName("home")]
    public Team Home { get; set; }

    [JsonPropertyName("away")]
    public Team Away { get; set; }
}