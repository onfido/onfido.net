using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Onfido.Errors
{
    public class ErrorField
    {
        [JsonProperty("messages")]
        public IEnumerable<string> Messages;
    }
}
