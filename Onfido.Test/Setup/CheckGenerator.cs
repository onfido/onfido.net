using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;

namespace Onfido.Test.Setup
{
    public class CheckGenerator
    {
        public const string ApplicantId = "1030303-123123-123123";

        public const string CheckId = "8546921-123123-123123";
        
        public static Check Check()
        {
            return new Check
            {
                Type = CheckType.Standard,
                Reports = new List<Report>
                {
                    new Report { Name = "identity" },
                    new Report { Name = "document" }
                }
            };
        }

        public static string Json()
        {
            return @"{
                  ""id"": ""8546921-123123-123123"",
                  ""created_at"": ""2014-05-23T13:50:33Z"",
                  ""href"": ""/v1/applicants/1030303-123123-123123/checks/8546921-123123-123123"",
                  ""type"": ""standard"",
                  ""status"": ""pending"",
                  ""result"": ""pending"",
                  ""reports"": [
                    {
                      ""id"": ""6951786-123123-422221"",
                      ""name"": ""identity"",
                      ""created_at"": ""2014-05-23T13:50:33Z"",
                      ""status"": ""awaiting_applicant"",
                      ""result"": ""pending"",
                      ""href"": ""/v1/checks/8546921-123123-123123/reports/6951786-123123-422221"",
                      ""breakdown"": {},
                      ""properties"": {}
                    },
                    {
                      ""id"": ""6951786-123123-316712"",
                      ""name"": ""document"",
                      ""created_at"": ""2014-05-23T13:50:33Z"",
                      ""status"": ""awaiting_applicant"",
                      ""result"": ""pending"",
                      ""href"": ""/v1/checks/8546921-123123-123123/reports/6951786-123123-316712"",
                      ""breakdown"": {},
                      ""properties"": {}
                    }
                  ]
                }";
        }

        public static string JsonList()
        {
            return @"{""reports"":[" + Json() + "]}";
        }
    }
}
