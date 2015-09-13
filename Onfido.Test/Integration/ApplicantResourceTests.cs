using System;
using System.Net.Http;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onfido.Http;
using Onfido.Resources;
using Onfido.Test.Setup;
using Rhino.Mocks;

namespace Onfido.Test.Integration
{
    public abstract class ApplicantsTestBase
    {
        protected Applicants ApplicantService { get; set; }

        protected IOnfidoHttpClient HttpClient { get; set; }

        protected Uri UriUsed { get; set; }

        protected int PageUsed { get; set; }

        protected int PerPageUsed { get; set; }

        public ApplicantsTestBase()
        {
            HttpClient = MockRepository.GenerateStub<IOnfidoHttpClient>();

            ApplicantService = new Applicants(new Requestor(HttpClient));

            PageUsed = 1;

            PerPageUsed = 10;
        }

        [TestMethod]
        public void Should_have_used_correct_url()
        {
            Assert.AreEqual(UriUsed.Host, Onfido.Settings.Hostname);
        }

        [TestMethod]
        public virtual void Should_have_called_correct_endpoint()
        {
            throw new NotImplementedException("test not implemented! needs overridden in derived class");
        }
    }

    [TestClass]
    public class When_calling_Applicants_create_service : ApplicantsTestBase {

        public When_calling_Applicants_create_service() : base()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ApplicantGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Applicant create should use POST"); });
        }

        [TestInitialize]
        public void CallService()
        {
            var applicant = ApplicantGenerator.Applicant(false);

            ApplicantService.Create(applicant);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/{1}", Onfido.Settings.ApiVersion, "applicants"));
        }
    }

    [TestClass]
    public class When_calling_Applicants_find_service : ApplicantsTestBase
    {
        private const string _applicantFindId = "test123";

        public When_calling_Applicants_find_service() : base()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ApplicantGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Applicant find should use GET"); });                
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
        }

        [TestInitialize]
        public void CallService()
        {
            ApplicantService.Find(_applicantFindId);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/{1}/{2}", Onfido.Settings.ApiVersion, "applicants", _applicantFindId));
        }
    }

    [TestClass]
    public class When_calling_Applicants_all_service : ApplicantsTestBase
    {
        public When_calling_Applicants_all_service() : base()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ApplicantGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Applicant all should use GET"); });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
        }

        [TestInitialize]
        public void CallService()
        {
            ApplicantService.All();
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            var pathQuerySplit = UriUsed.PathAndQuery.Split('?');
            Assert.AreEqual(pathQuerySplit.Length, 2);

            var path = pathQuerySplit[0];
            Assert.AreEqual(path, string.Format("/{0}/{1}", Onfido.Settings.ApiVersion, "applicants"));

            var query = HttpUtility.ParseQueryString(pathQuerySplit[1]);
            Assert.AreEqual(Int32.Parse(query["page"]), PageUsed);
            Assert.AreEqual(Int32.Parse(query["per_page"]), PerPageUsed);
        }
    }

    [TestClass]
    public class When_calling_Applicants_all_service_with_pagination : ApplicantsTestBase
    {
        public When_calling_Applicants_all_service_with_pagination() : base()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ApplicantGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Applicant all should use GET"); });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });

            PageUsed = 2;
            PerPageUsed = 20;
        }

        [TestInitialize]
        public void CallService()
        {
            ApplicantService.All(PageUsed, PerPageUsed);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            var pathQuerySplit = UriUsed.PathAndQuery.Split('?');
            Assert.AreEqual(pathQuerySplit.Length, 2);

            var path = pathQuerySplit[0];
            Assert.AreEqual(path, string.Format("/{0}/{1}", Onfido.Settings.ApiVersion, "applicants"));

            var query = HttpUtility.ParseQueryString(pathQuerySplit[1]);
            Assert.AreEqual(Int32.Parse(query["page"]), PageUsed);
            Assert.AreEqual(Int32.Parse(query["per_page"]), PerPageUsed);
        }
    }
    }
