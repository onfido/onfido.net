using System;
using Newtonsoft.Json;

namespace Onfido.Entities
{
    public class Address
    {
        [JsonProperty("flat_number")]
        public string FlatNumber;

        [JsonProperty("building_number")]
        public string BuildingNumber;

        [JsonProperty("street")]
        public string Street;

        [JsonProperty("sub_street")]
        public string SubStreet;

        [JsonProperty("town")]
        public string Town;

        [JsonProperty("state")]
        public string State;

        [JsonProperty("postcode")]
        public string Postcode;

        [JsonProperty("country")]
        public string Country;

        [JsonProperty("start_date")]
        public DateTime? StartDate;

        [JsonProperty("end_date")]
        public DateTime? EndDate;
    }
}
