using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onfido.Http
{
    public interface IRequestor
    {
        T Get<T>(string url);

        T Get<T>(string url, NameValueCollection query);

        T Post<T>(string url, string payload);
    }
}
