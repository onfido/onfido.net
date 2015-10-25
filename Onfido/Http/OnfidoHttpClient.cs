using System;
using System.Net.Http;
using Newtonsoft.Json;
using Onfido.Resources.InternalEntities;
using Onfido.Errors;

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

            request.Headers.Add("Authorization", string.Format("Token token={0}", Settings.GetApiToken()));

            var response = _http.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var errorResponse = JsonConvert.DeserializeObject<OnfidoExceptionResponse>(response.Content.ReadAsStringAsync().Result);

                    var e = new OnfidoApiException
                    {
                        Id = errorResponse.Error.Id,
                        Type = errorResponse.Error.Type,
                        ErrorMessage = errorResponse.Error.ErrorMessage,
                        Fields = errorResponse.Error.Fields
                    };
                    throw e;
                }

                throw new Exception("An error occurred communicating with Onfido");
            }

            return response;
        }
    }
}
