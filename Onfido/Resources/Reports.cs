using Onfido.Entities;
using Onfido.Http;

namespace Onfido.Resources
{
    public class Reports
    {
        private IRequestor _requestor;

        public Reports() : this(new Requestor())
        {
        }

        public Reports(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Report Find(string checkId, string reportId)
        {
            const string pathFormat = "checks/{0}/reports/{1}";

            return _requestor.Get<Report>(string.Format(pathFormat, checkId, reportId));           
        }

        public Report All(string checkId)
        {
            const string pathFormat = "checks/{0}/reports";

            return _requestor.Get<Report>(string.Format(pathFormat, checkId));
        }
    }
}
