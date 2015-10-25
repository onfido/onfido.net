using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Onfido.Errors
{
    public class OnfidoApiException : Exception
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string ErrorMessage { get; set; }

        public Dictionary<string, IEnumerable<string>> Fields { get; set; }

        public override string ToString()
        {
            var errorString = new StringBuilder();

            errorString.AppendLine(string.Format("error id: {0}\nerror type: {1}\nmessage: {2}", Id, Type, ErrorMessage));

            foreach (var field in Fields.Keys)
            {
                errorString.AppendLine(string.Format("{0}:", field));

                foreach (var fieldError in Fields[field])
                {
                    errorString.AppendLine(string.Format("- {0}", fieldError));
                }
            }

            errorString.AppendLine(StackTrace);

            return errorString.ToString();
        }
    }
}
