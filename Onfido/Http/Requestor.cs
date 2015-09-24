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
            var builder = getUriBuilder(path);
            builder.Query = query != null ? query.ToString() : null;

            var response = _http.Get(builder.Uri);

            var responseText = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(responseText);
        }

        public T Post<T>(string path, string jsonPayload)
        {
            return Post<T>(path, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
        }

        public T Post<T>(string path, HttpContent payload)
        {
            var builder = getUriBuilder(path);

            var response = _http.Post(builder.Uri, payload);

            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        private UriBuilder getUriBuilder(string path)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host =  Onfido.Settings.Hostname,
                Path = string.Format("{0}/{1}", Onfido.Settings.ApiVersion, path)
            };

            return uriBuilder;
        }
    }
}
