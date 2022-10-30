using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Fixtures;

public class Score
{
    [JsonPropertyName("halftime")]
    public MatchScore Halftime { get; set; }

    [JsonPropertyName("fulltime")]
    public MatchScore Fulltime { get; set; }

    [JsonPropertyName("extratime")]
    public MatchScore Extratime { get; set; }

    [JsonPropertyName("penalty")]
    public MatchScore Penalty { get; set; }
}