using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Onfido.Http
{
    public class Requestor : IRequestor
    {        
        private const string _defaultOnfidoUrl = "api.onfido.com/v1/";

        private IOnfidoHttpClient _http;

        private string _baseUrl;
    
        public Requestor() : this(new OnfidoHttpClient(), _defaultOnfidoUrl)
        {
        }

        public Requestor(IOnfidoHttpClient http, string baseUrl)
        {
            _http = http;

            _baseUrl = baseUrl;
        }

        public T Get<T>(string path)
        {
            return Get<T>(path, null);
        }

        public T Get<T>(string path, NameValueCollection query)
        {
            var uriBuilder = new UriBuilder()
            {
                Scheme = Uri.UriSchemeHttps,
                Host = _baseUrl,
                Path = path,
                Query = query.ToString(),
            };

            var response = _http.Get(uriBuilder.Uri);

            return JsonConvert.DeserializeObject<T>(response.Content.ToString());
        }

        public T Post<T>(string path)
        {
            throw new NotImplementedException();
        }
    }
}
