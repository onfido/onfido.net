using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;
using Onfido.Http;

namespace Onfido.Resources
{
    public class CreateDocument
    {

    }

    public class Documents
    {
        private IRequestor _requestor;

        public Documents() : this(new Requestor())
        {
        }

        public Documents(IRequestor requestor)
        {
            _requestor = requestor;
        }

        public Document Create(string applicantId, Stream fileStream, DocumentType type)
        {
            return Create(applicantId, ReadAllBytes(fileStream), type);
        }

        public Document Create(string applicantId, byte[] fileBytes, DocumentType type)
        {
            return Create(applicantId, new ByteArrayContent(fileBytes), type);
        }

        public Document Create(string applicantId, ByteArrayContent fileByteArrayContent, DocumentType type)
        {
            throw new NotImplementedException();
        }

        private static byte[] ReadAllBytes(Stream reader)
        {
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }
        }
    }
}
