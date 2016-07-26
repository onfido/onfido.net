using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onfido
{
    public static class Settings
    {
        public const string Hostname = "api.onfido.com";

        private static string _apiVersion = "v2";

        private static string _apiToken;

        public static string GetApiToken()
        {
            // TODO: if string.isnullorempty(token) throw an exception?
            //       disruptive, but maybe better than doing nothing and
            //       leaving dev to scratch his/her head.
            return _apiToken;
        }

        public static void SetApiToken(string apiToken)
        {
            _apiToken = apiToken;
        }

        public static void SetApiVersion(string version)
        {
            _apiVersion = version;
        }

        public static string GetApiVersion()
        {
            return _apiVersion;
        }
    }
}
