using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onfido.Entities;

namespace Onfido.Test.Setup
{
    public class IdNumberGenerator
    {
        /*
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
        */

        public static IdNumber IdNumber1()
        {
            return new IdNumber
            {
                Type = "ssn",
                Value = "433-54-3937"                       
            };
        }

        public static string Json1()
        {
            return @"{
                  ""type"": ""ssn"",
                  ""value"": ""433-54-3937""
            }";
        }

        public static IdNumber IdNumber2()
        {
            return new IdNumber
            {
                Type = "driving_license",
                Value = "I1234562",
                State = "CA"
            };
        }

        public static string Json2()
        {
            return @"
                  ""type"": ""driving_license"",
                  ""value"": ""I1234562"",
                  ""state"": ""CA""
            ";
        }
    }
}
