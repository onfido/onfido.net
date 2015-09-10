using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Onfido.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CheckType
    {
        [EnumMember(Value = "standard")]
        Standard,
        [EnumMember(Value = "express")]
        Express
    }

    public class Check
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;

        [JsonProperty("href")]
        public string HRef;

        [JsonProperty("type")]
        public CheckType Type;

        [JsonProperty("result")]
        public string Result;

        [JsonProperty("reports")]
        public IEnumerable<Report> Reports;
    }
}
