using Onfido.Entities;

namespace Onfido.Test.Setup
{
    public class AddressGenerator
    {
        public static Address Address1()
        {
            return new Address
            {
                BuildingNumber = "100",
                Street = "Main Street",
                Town = "London",
                Postcode = "SW4 6EH",
                Country = "GBR",
                StartDate = new System.DateTime(2013, 1, 1)
            };
        }

        public static string Json1()
        {
            return @"{
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
            }";
        }

        public static Address Address2()
        {
            return new Address
            {
                FlatNumber = "Apt 2A",
                BuildingNumber = "1017",
                Street = "Oakland Ave",
                Town = "Piedmont",
                State = "CA",
                Postcode = "94611",
                Country = "USA",
                StartDate = new System.DateTime(2006, 3, 7),
                EndDate = new System.DateTime(2012, 12, 31)
            };
        }

        public static string Json2()
        {
            return @"{
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
            }";
        }
    }
}
