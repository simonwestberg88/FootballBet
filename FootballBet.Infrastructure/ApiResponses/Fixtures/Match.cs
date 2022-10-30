using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Fixtures;

public class Match
{
    [JsonPropertyName("fixture")]
    public Fixture Fixture { get; set; }

    [JsonPropertyName("league")]
    public League League { get; set; }

    [JsonPropertyName("teams")]
    public Teams Teams { get; set; }

    [JsonPropertyName("goals")]
    public MatchScore Goals { get; set; }

    [JsonPropertyName("score")]
    public Score Score { get; set; }
}