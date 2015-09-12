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
            return Send(HttpMethod.Get, uri, null);
        }

        public HttpResponseMessage Post(Uri uri, HttpContent payload)
        {
            return Send(HttpMethod.Post, uri, payload);
        }

        private HttpResponseMessage Send(HttpMethod method, Uri uri, HttpContent content)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = method,
                Content = content
            };

            request.Headers.Add("Authorization", string.Format("Token token={0}", Configuration.GetApiToken()));

            return _http.SendAsync(request).Result;
        }
    }
}
