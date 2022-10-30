using System.Text.Json.Serialization;
using FootballBet.Server.Models.Football.Leagues;

namespace FootballBet.Server.Models.Football.ApiResponses.Leagues;

public class LeaguesRoot
{
    [JsonPropertyName("get")]
    public string Get { get; set; }

    [JsonPropertyName("parameters")]
    public Parameters Parameters { get; set; }

    [JsonPropertyName("errors")]
    public List<object> Errors { get; set; }

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("paging")]
    public Paging Paging { get; set; }

    [JsonPropertyName("response")]
    public List<Response> Response { get; set; }
}