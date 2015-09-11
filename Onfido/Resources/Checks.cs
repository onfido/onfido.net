using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;
using Onfido.Http;

namespace Onfido.Services
{
    public class Checks
    {
        private IRequestor _requestor;

        public Checks() : this(new Requestor())
        {
        }

        public Checks(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Check Create()
        {
            const string pathFormat = "applicants/{0}/checks";

            throw new NotImplementedException();
        }

        public Check Find(string applicantId, string checkId)
        {
            const string pathFormat = "applicants/{0}/checks/{1}";

            return _requestor.Get<Check>(string.Format(pathFormat, applicantId, checkId));
        }

        public IEnumerable<Check> All(string applicantId)
        {
            const string pathFormat = "applicants/{0}";

            return _requestor.Get<IEnumerable<Check>>(string.Format(pathFormat, applicantId));
        }
    }
}
