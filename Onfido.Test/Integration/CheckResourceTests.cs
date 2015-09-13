
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onfido.Http;
using Onfido.Resources;
using Onfido.Test.Setup;
using Rhino.Mocks;

namespace Onfido.Test.Integration
{
    public abstract class ChecksTestBase
    {
        protected Checks CheckService { get; set; }

        protected IOnfidoHttpClient HttpClient { get; set; }

        protected Uri UriUsed { get; set; }

        protected int PageUsed { get; set; }

        protected int PerPageUsed { get; set; }

        public ChecksTestBase()
        {
            HttpClient = MockRepository.GenerateStub<IOnfidoHttpClient>();

            CheckService = new Checks(new Requestor(HttpClient));

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
    public class When_calling_Checks_create_service : ChecksTestBase
    {
        public When_calling_Checks_create_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(CheckGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Check create should use POST"); });
        }

        [TestInitialize]
        public void CallService()
        {
            CheckService.Create(CheckGenerator.ApplicantId, CheckGenerator.Check());
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/applicants/{1}/checks", Settings.ApiVersion, CheckGenerator.ApplicantId));
        }
    }

    [TestClass]
    public class When_calling_Checks_find_service : ChecksTestBase
    {
        public When_calling_Checks_find_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(CheckGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Check find should use GET"); });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
        }

        [TestInitialize]
        public void CallService()
        {
            CheckService.Find(CheckGenerator.ApplicantId, CheckGenerator.CheckId);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/applicants/{1}/checks/{2}", Settings.ApiVersion, CheckGenerator.ApplicantId, CheckGenerator.CheckId));
        }
    }


    [TestClass]
    public class When_calling_Checks_all_service : ChecksTestBase
    {
        public When_calling_Checks_all_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(CheckGenerator.JsonList());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Check all should use GET"); });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
        }

        [TestInitialize]
        public void CallService()
        {
            CheckService.All(CheckGenerator.ApplicantId);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/applicants/{1}/checks", Settings.ApiVersion, CheckGenerator.ApplicantId));
        }
    }
}
