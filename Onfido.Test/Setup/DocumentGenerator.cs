using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;

namespace Onfido.Test.Setup
{
    public static class DocumentGenerator
    {
        public const string ApplicantId = "1030303-1231x23-123123";

        public const string Filename = "passport.png";

        public static Stream FileStream()
        {
            return new FileStream("Setup/"+Filename, FileMode.Open);
        }

        public static DocumentType DocumentType = DocumentType.Passport;

        public static string Json()
        {
            return @"{
                ""id"": ""7568415 - 123123 - 123123"",
                ""created_at"": ""2014-05-23 13:50:33Z"",
                ""href"": ""/v1/applicants/1030303-123123-123123/documents/7568415-123123-123123"",
                ""file_name"": ""localfile.png"",
                ""file_type"": ""png"",
                ""file_size"": 282870,
                ""type"": ""passport"",
                ""side"": null
            }";
        }
    }
}
