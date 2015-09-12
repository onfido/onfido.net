using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Onfido.Entities;
using Onfido.Test.Setup;

namespace Onfido.Test.Serialization
{
    [TestClass]
    public class WhenDeserializingAnApplicant
    {
        [TestMethod]
        public void ShouldDeserializeApplicantAsExpected()
        {
            var applicant = JsonConvert.DeserializeObject<Applicant>(ApplicantGenerator.Json());
            var applicantRef = ApplicantGenerator.Applicant(true);

            Assert.AreEqual(applicant.Id, applicantRef.Id);
            Assert.AreEqual(applicant.CreatedAt, applicantRef.CreatedAt);
            Assert.AreEqual(applicant.HRef, applicantRef.HRef);
            Assert.AreEqual(applicant.Title, applicantRef.Title);
            Assert.AreEqual(applicant.FirstName, applicantRef.FirstName);
            Assert.AreEqual(applicant.MiddleName, applicantRef.MiddleName);
            Assert.AreEqual(applicant.LastName, applicantRef.LastName);
            Assert.AreEqual(applicant.Gender, applicantRef.Gender);
            Assert.AreEqual(applicant.DateOfBirth, applicantRef.DateOfBirth);
            Assert.AreEqual(applicant.Telephone, applicantRef.Telephone);
            Assert.AreEqual(applicant.Mobile, applicantRef.Mobile);
            Assert.AreEqual(applicant.Country, applicantRef.Country);

            Assert.AreEqual(applicant.IdNumbers.Count(), applicantRef.IdNumbers.Count());

            var ssn = new IdNumber { Type = "ssn", Value = "433-54-3937", State = null };
            var driving_license = new IdNumber { Type = "driving_license", Value = "I1234562", State = "CA" };
            var found_ssn = false;
            var found_driving_license = false;

            foreach (var id in applicant.IdNumbers)
            {
                found_ssn |= CompareIdNumbers(id, applicantRef.IdNumbers.First());
                found_driving_license |= CompareIdNumbers(id, applicantRef.IdNumbers.Last());
            }

            Assert.IsTrue(found_ssn);
            Assert.IsTrue(found_driving_license);

            var found_current_addr = false;
            var found_previous_addr = false;

            foreach (var address in applicant.Addresses)
            {
                found_current_addr |= CompareAddresses(address, applicantRef.Addresses.First());
                found_previous_addr |= CompareAddresses(address, applicantRef.Addresses.Last());
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
