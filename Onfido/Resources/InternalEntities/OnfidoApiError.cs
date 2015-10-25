using System.Collections.Generic;
using Newtonsoft.Json;

namespace Onfido.Resources.InternalEntities
{
    public class OnfidoApiError
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, IEnumerable<string>> Fields { get; set; }
    }
}
