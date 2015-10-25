using System;
using System.IO;
using System.Net.Http;
using Onfido.Entities;
using Onfido.Http;
using System.Net.Http.Headers;
using Onfido.Resources.InternalEntities;
using System.Text;
using System.Linq;

namespace Onfido.Resources
{
    public class Documents : OnfidoResource
    {
        private IRequestor _requestor;

        public Documents() : this(new Requestor())
        {
        }

        public Documents(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Document Create(string applicantId, Stream fileStream, string fileName, DocumentType type)
        {
            return Create(applicantId, fileStream, fileName, type, null);
        }


        public Document Create(string applicantId, Stream fileStream, string fileName, DocumentType type, DocumentSide? side)
        {
            const string pathFormat = "applicants/{0}/documents";
            
            var mimeType = System.Web.MimeMapping.GetMimeMapping(fileName);

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(Utilities.EnumHelper.GetDescription(type)), "\"type\"");

                if (side != null)
                {
                    formData.Add(new StringContent(Utilities.EnumHelper.GetDescription(side.Value), Encoding.UTF8), "\"side\"");
                }

                formData.Add(CreateFileContent(fileStream, "file", mimeType));

                return _requestor.Post<Document>(string.Format(pathFormat, applicantId), formData);
            }
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = fileName
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return fileContent;
        }

    }
}