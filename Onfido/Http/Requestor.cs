using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Onfido.Http
{
    public class Requestor : IRequestor
    {        
        private IOnfidoHttpClient _http;
        
        public Requestor() : this(new OnfidoHttpClient())
        {
        }

        public Requestor(IOnfidoHttpClient http)
        {
            _http = http;            
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
                Host = Onfido.Settings.Hostname,
                Path = string.Format("{0}/{1}", Onfido.Settings.ApiVersion, path),
                Query = query != null ? query.ToString() : null
            };

            var response = _http.Get(uriBuilder.Uri);

            var responseText = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(responseText);
        }

        public T Post<T>(string path, string jsonPayload)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = Onfido.Settings.Hostname,
                Path = string.Format("{0}/{1}", Onfido.Settings.ApiVersion, path)
            };

            var response = _http.Post(uriBuilder.Uri, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
            
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
