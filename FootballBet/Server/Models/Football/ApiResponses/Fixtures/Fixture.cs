﻿using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Fixture
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("referee")]
        public string Referee { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("periods")]
        public Periods Periods { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
