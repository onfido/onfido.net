using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;

namespace Onfido.Test.Setup
{
    class ReportGenerator
    {
        public const string CheckId = "8546921-123123-123123";

        public static Report Report()
        {
            return new Report
            {
                Id = "6951786-123123-316712",
                Name = "identity",
                CreatedAt = new DateTime(2014, 5, 23, 13, 50, 33),
                Status = "awaiting_data",
                Result = "pending",
                HRef = "/v1/checks/8546921-123123-123123/reports/6951786-123123-316712",
                Breakdown = new Dictionary<string, string>(),
                Properties = new Dictionary<string, string>(),
            };
        }

        public static string Json()
        {
            return @"{
                ""id"": ""6951786 - 123123 - 316712"",
                ""name"": ""identity"",
                ""created_at"": ""2014-05-23T13:50:33Z"",
                ""status"": ""awaiting_data"",
                ""result"": ""pending"",
                ""href"": ""/v1/checks/8546921-123123-123123/reports/6951786-123123-316712"",
                ""breakdown"": { },
                ""properties"": { }
            }";
        }

        public static string JsonArray()
        {
            return @"{
                ""reports"" : [ " 
                + Json() + @"
            ]}";
        }

        public static string ReturnedReportsForCheckTest()
        {
            return @"{
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
            }";
        }
    }
}
