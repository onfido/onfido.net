using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onfido.Resources;
using Onfido.Http;
using Onfido.Test.Setup;
using Rhino.Mocks;
using System.Net.Http;

namespace Onfido.Test.Integration
{
    public abstract class ReportsTestBase
    {
        protected Reports ReportsService { get; set; }

        protected IOnfidoHttpClient HttpClient { get; set; }

        protected Uri UriUsed { get; set; }

        public ReportsTestBase()
        {
            HttpClient = MockRepository.GenerateStub<IOnfidoHttpClient>();

            ReportsService = new Reports(new Requestor(HttpClient));
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
    public class When_calling_reports_find_service : ReportsTestBase
    {
        public When_calling_reports_find_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ReportGenerator.Json());

            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Reports find should use GET"); });
        }

        [TestInitialize]
        public void CallService()
        {
            ReportsService.Find(ReportGenerator.CheckId, ReportGenerator.Report().Id);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/checks/{1}/reports/{2}", Settings.ApiVersion, ReportGenerator.CheckId, ReportGenerator.Report().Id));
        }
    }

    [TestClass]
    public class When_calling_reports_all_service : ReportsTestBase
    {
        public When_calling_reports_all_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(ReportGenerator.JsonArray());

            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Reports all should use GET"); });
        }

        [TestInitialize]
        public void CallService()
        {
            ReportsService.All(ReportGenerator.CheckId);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/checks/{1}/reports", Settings.ApiVersion, ReportGenerator.CheckId));
        }
    }
}
