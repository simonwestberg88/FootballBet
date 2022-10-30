using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Parameters
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    


}
