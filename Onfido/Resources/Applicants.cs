using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Onfido.Entities;
using Onfido.Http;
using System.Collections.Specialized;

namespace Onfido.Resources
{
    public class CreateApplicant
    {
        [JsonProperty("title")]
        public string Title;

        [JsonProperty("first_name")]
        public string FirstName;

        [JsonProperty("middle_name")]
        public string MiddleName;

        [JsonProperty("last_name")]
        public string LastName;

        [JsonProperty("gender")]
        public string Gender;

        [JsonProperty("dob")]
        public DateTime DateOfBirth;

        [JsonProperty("telephone")]
        public string Telephone;

        [JsonProperty("mobile")]
        public string Mobile;

        [JsonProperty("country")]
        public string Country;

        [JsonProperty("id_numbers")]
        public IEnumerable<IdNumber> IdNumbers;

        [JsonProperty("addresses")]
        public IEnumerable<Address> Addresses;
    }

    public class Applicants
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

        public Applicant Create(CreateApplicant applicant)
        {
            const string path = "applicants";

            var payload = JsonConvert.SerializeObject(applicant);

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

            var query = new NameValueCollection();

            query["page"] = page.ToString();
            query["per_page"] = perPage.ToString();

            return _requestor.Get<IEnumerable<Applicant>>(path, query);
        }
    }
}