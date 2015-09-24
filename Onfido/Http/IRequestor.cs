using System.Collections.Specialized;
using System.Net.Http;

namespace Onfido.Http
{
    public interface IRequestor
    {
        T Get<T>(string url);

        T Get<T>(string url, NameValueCollection query);

        T Post<T>(string path, HttpContent payload);

        T Post<T>(string url, string payload);
    }
}
