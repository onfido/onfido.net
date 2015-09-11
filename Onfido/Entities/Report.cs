using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Onfido.Entities
{
    public class Report
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("result")]
        public string Result;

        [JsonProperty("href")]
        public string HRef;

        [JsonProperty("breakdown")]
        public Dictionary<string, string> Breakdown;

        [JsonProperty("properties")]
        public Dictionary<string, string> Properties;
    }
}
