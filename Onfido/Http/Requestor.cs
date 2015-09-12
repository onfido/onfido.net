using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Onfido.Http
{
    public class Requestor : IRequestor
    {        
        private const string _defaultOnfidoUrl = "api.onfido.com/v1";

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

            var responseText = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(responseText);
        }

        public T Post<T>(string path, string jsonPayload)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = _baseUrl,
                Path = path
            };

            var response = _http.Post(uriBuilder.Uri, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
