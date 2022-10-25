using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Fixtures
{
    public class Fixture
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("referee")]
        public string Referee { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("periods")]
        public Periods Periods { get; set; }

        [JsonPropertyName("venue")]
        public Venue Venue { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }
}
