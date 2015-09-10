using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Onfido.Types;

namespace Onfido.Test.Serialization
{

    /* a horrible and quick test to get started with */
    [TestClass]
    public class WhenDeserializingAnApplicant
    {
        private string _inputApplicant = @"{
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

        [TestMethod]
        public void ShouldDeserializeApplicantAsExpected()
        {
            var applicant = JsonConvert.DeserializeObject<Applicant>(_inputApplicant);

            Assert.AreEqual(applicant.Id, "1030303-123123-123123");
            Assert.AreEqual(applicant.CreatedAt, new DateTime(2014, 5, 23, 13, 50, 33));
            Assert.AreEqual(applicant.HRef, "/v1/applicants/1030303-123123-123123");
            Assert.AreEqual(applicant.Title, "Mr");
            Assert.AreEqual(applicant.FirstName, "John");
            Assert.IsNull(applicant.MiddleName);
            Assert.AreEqual(applicant.LastName, "Smith");
            Assert.AreEqual(applicant.Gender, "male");
            Assert.AreEqual(applicant.DateOfBirth, new DateTime(2013, 2, 17));
            Assert.AreEqual(applicant.Telephone, "02088909293");
            Assert.IsNull(applicant.Mobile);
            Assert.AreEqual(applicant.Country, "GBR");

            Assert.AreEqual(applicant.IdNumbers.Count(), 2);

            var ssn = new IdNumber { Type = "ssn", Value = "433-54-3937", State = null };
            var driving_license = new IdNumber { Type = "driving_license", Value = "I1234562", State = "CA" };
            var found_ssn = false;
            var found_driving_license = false;

            foreach (var id in applicant.IdNumbers)
            {
                found_ssn |= CompareIdNumbers(id, ssn);
                found_driving_license |= CompareIdNumbers(id, driving_license);
            }

            Assert.IsTrue(found_ssn);
            Assert.IsTrue(found_driving_license);

            var current_addr = new Address
            {
                BuildingNumber = "100",
                Street = "Main Street", 
                Town = "London",
                Postcode = "SW4 6EH",
                Country = "GBR",
                StartDate = new DateTime(2013, 01, 01)
            };

            var previous_addr = new Address
            {
                FlatNumber = "Apt 2A",
                BuildingNumber = "1017",
                Street = "Oakland Ave",
                Town = "Piedmont",
                State = "CA",
                Postcode = "94611",
                Country = "USA",
                StartDate = new DateTime(2006, 3, 7),
                EndDate = new DateTime(2012, 12, 31)
            };

            var found_current_addr = false;
            var found_previous_addr = false;

            foreach (var address in applicant.Addresses)
            {
                found_current_addr |= CompareAddresses(address, current_addr);
                found_previous_addr |= CompareAddresses(address, previous_addr);
            }

            Assert.IsTrue(found_current_addr);
            Assert.IsTrue(found_previous_addr);
        }

        private bool CompareIdNumbers(IdNumber id1, IdNumber id2)
        {
            return id1.Type == id2.Type &&
                id1.Value == id2.Value &&
                id1.State == id2.State;
        }

        private bool CompareAddresses(Address address1, Address address2)
        {
            return address1.BuildingNumber == address2.BuildingNumber &&
                address1.Country == address2.Country &&
                address1.EndDate == address2.EndDate &&
                address1.FlatNumber == address2.FlatNumber &&
                address1.Postcode == address2.Postcode &&
                address1.StartDate == address2.StartDate &&
                address1.State == address2.State &&
                address1.Street == address2.Street &&
                address1.SubStreet == address2.SubStreet &&
                address1.Town == address2.Town;
        }
    }
}
