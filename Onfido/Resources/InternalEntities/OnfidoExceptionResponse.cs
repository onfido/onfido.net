using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Utilities;
using Newtonsoft.Json;
using Onfido.Errors;

namespace Onfido.Resources.InternalEntities
{
    public class OnfidoExceptionResponse
    {
        [JsonProperty("error")]
        public OnfidoApiError Error;
    }
}
