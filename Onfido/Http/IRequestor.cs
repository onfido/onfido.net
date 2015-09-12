using System.Collections.Specialized;

namespace Onfido.Http
{
    public interface IRequestor
    {
        T Get<T>(string url);

        T Get<T>(string url, NameValueCollection query);

        T Post<T>(string url, string payload);
    }
}
