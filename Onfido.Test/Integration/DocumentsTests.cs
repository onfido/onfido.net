
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
    public abstract class DocumentsTestBase
    {
        protected Documents DocumentService { get; set; }

        protected IOnfidoHttpClient HttpClient { get; set; }

        protected Uri UriUsed { get; set; }

        public DocumentsTestBase()
        {
            HttpClient = MockRepository.GenerateStub<IOnfidoHttpClient>();

            DocumentService = new Documents(new Requestor(HttpClient));
        }

        [TestMethod]
        public void Should_have_used_correct_url()
        {
            Assert.AreEqual(UriUsed.Host, Onfido.Settings.Hostname);
        }

        [TestMethod]
        public virtual void Should_have_called_correct_endpoint()
        {
            throw new NotImplementedException("Test not implemented! needs overridden in derived class");
        }
    }

    [TestClass]
    public class When_calling_Documents_create_service : DocumentsTestBase
    {
        public When_calling_Documents_create_service()
        {
            var stubResponse = new HttpResponseMessage();
            stubResponse.Content = new StringContent(DocumentGenerator.Json());

            HttpClient.Stub(client => client.Post(Arg<Uri>.Is.Anything, Arg<HttpContent>.Is.Anything))
                .Return(stubResponse)
                .WhenCalled(c => { UriUsed = (Uri)c.Arguments[0]; });
            HttpClient.Stub(client => client.Get(Arg<Uri>.Is.Anything))
                .WhenCalled(c => { throw new Exception("Document create should use POST"); });
        }

        [TestInitialize]
        public void CallService()
        {
            DocumentService.Create(DocumentGenerator.ApplicantId, DocumentGenerator.FileStream(), DocumentGenerator.Filename, DocumentGenerator.DocumentType);
        }

        [TestMethod]
        public override void Should_have_called_correct_endpoint()
        {
            Assert.AreEqual(UriUsed.PathAndQuery, string.Format("/{0}/applicants/{1}/documents", Settings.GetApiVersion(), DocumentGenerator.ApplicantId));
        }
    }
}
