using System.Collections.Generic;
using Onfido.Resources.InternalEntities;
using Onfido.Entities;
using Onfido.Http;
using System.Collections.Specialized;
using System.Web;

namespace Onfido.Resources
{
    public class Applicants : OnfidoResource
    {
        private const int _defaultPage = 1;

        private const int _defaultPerPage = 10;

        private IRequestor _requestor;

        public Applicants() : this(new Requestor())
        {
        }

        public Applicants(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Applicant Create(Applicant applicant)
        {
            const string path = "applicants";

            var payload = SerializeEntity(applicant);

            return _requestor.Post<Applicant>(path, payload);
        }

        public Applicant Find(string applicantId)
        {
            const string pathFormat = "applicants/{0}";

            return _requestor.Get<Applicant>(string.Format(pathFormat, applicantId));
        }

        public IEnumerable<Applicant> All()
        {
            return All(_defaultPage, _defaultPerPage);
        }

        public IEnumerable<Applicant> All(int page, int perPage)
        {
            const string path = "applicants";

            var query = HttpUtility.ParseQueryString(string.Empty);

            query["page"] = page.ToString();
            query["per_page"] = perPage.ToString();

            var applicants = _requestor.Get<ApplicantResponse>(path, query);

            return applicants.applicants;
        }
    }
}