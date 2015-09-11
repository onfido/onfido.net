using System;
using System.Net.Http;

namespace Onfido.Http
{
    public class OnfidoHttpClient : IOnfidoHttpClient
    {
        private HttpClient _http;

        public OnfidoHttpClient() :this(new HttpClient())
        {
        }

        public OnfidoHttpClient(HttpClient http)
        {
            _http = http;
        }

        public HttpResponseMessage Get(Uri uri)
        {
            return _http.GetAsync(uri).Result;
        }

        public HttpResponseMessage Post(Uri uri, HttpContent payload)
        {
            return _http.PostAsync(uri, payload).Result;
        }
    }
}
