using Onfido.Entities;
using System.Collections.Generic;

namespace Onfido.Test.Setup
{
    public class ApplicantGenerator
    {
        public static Applicant Applicant(bool created)
        {
            var applicant = new Applicant
            {
                Title = "Mr",
                FirstName = "John",
                LastName = "Smith",
                Gender = "male",
                DateOfBirth = new System.DateTime(2013, 2, 17),
                Telephone = "02088909293",
                Country = "GBR"
            };

            if (created)
            {
                applicant.Id = "1030303-123123-123123";
                applicant.CreatedAt = new System.DateTime(2014, 5, 23, 13, 50, 33);
                applicant.HRef = "/v1/applicants/1030303-123123-123123";
            }

            applicant.Addresses = new List<Address>
            {
                AddressGenerator.Address1(),
                AddressGenerator.Address2()
            };

            applicant.IdNumbers = new List<IdNumber>
            {
                IdNumberGenerator.IdNumber1(),
                IdNumberGenerator.IdNumber2()
            };

            return applicant;
        }

        public static string Json()
        {
            return @"{
              ""id"": ""1030303-123123-123123"",
              ""created_at"": ""2014-05-23T13:50:33Z"",
              ""href"": ""/v1/applicants/1030303-123123-123123"",
              ""title"": ""Mr"",
              ""first_name"": ""John"",
              ""middle_name"": null,
              ""last_name"": ""Smith"",
              ""gender"": ""male"",
              ""dob"": ""2013-02-17"",
              ""telephone"": ""02088909293"",
              ""mobile"": null,
              ""country"": ""GBR"",
              ""id_numbers"": [
                {
                  ""type"": ""ssn"",
                  ""value"": ""433-54-3937""
                },
                {
                  ""type"": ""driving_license"",
                  ""value"": ""I1234562"",
                  ""state"": ""CA""
                }
              ],
              ""addresses"": [
                {
                  ""flat_number"": null,
                  ""building_name"": null,
                  ""building_number"": ""100"",
                  ""street"": ""Main Street"",
                  ""sub_street"": null,
                  ""state"": null,
                  ""town"": ""London"",
                  ""postcode"": ""SW4 6EH"",
                  ""country"": ""GBR"",
                  ""start_date"": ""2013-01-01"",
                  ""end_date"": null
                },
                {
                  ""flat_number"": ""Apt 2A"",
                  ""building_name"": null,
                  ""building_number"": ""1017"",
                  ""street"": ""Oakland Ave"",
                  ""sub_street"": null,
                  ""town"": ""Piedmont"",
                  ""state"": ""CA"",
                  ""postcode"": ""94611"",
                  ""country"": ""USA"",
                  ""start_date"": ""2006-03-07"",
                  ""end_date"": ""2012-12-31""
                }
              ]
            }";
        }
    }
}
