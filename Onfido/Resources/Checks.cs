using System.Collections.Generic;
using Onfido.Entities;
using Onfido.Http;
using Onfido.Resources.InternalEntities;

namespace Onfido.Resources
{
    public class Checks : OnfidoResource
    {
        private IRequestor _requestor;

        public Checks() : this(new Requestor())
        {
        }

        public Checks(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Check Create(string applicantId, Check check)
        {
            const string pathFormat = "applicants/{0}/checks";

            var payload = SerializeEntity(check);

            return _requestor.Post<Check>(string.Format(pathFormat, applicantId), payload);
        }

        public Check Find(string applicantId, string checkId)
        {
            const string pathFormat = "applicants/{0}/checks/{1}";

            return _requestor.Get<Check>(string.Format(pathFormat, applicantId, checkId));
        }

        public IEnumerable<Check> All(string applicantId)
        {
            const string pathFormat = "applicants/{0}/checks";

            var checks = _requestor.Get<CheckResponse>(string.Format(pathFormat, applicantId));

            return checks.checks;
        }
    }
}
