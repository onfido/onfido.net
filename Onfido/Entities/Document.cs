using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace Onfido.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DocumentType {
        [Description("passport")]
        Passport,
        [Description("national_identity_card")]
        NationalIdentityCard,
        [Description("work_permit")]
        WorkPermit,
        [Description("driving_license")]
        DrivingLicense,
        [Description("national_insurance")]
        NationalInsurance,
        [Description("birth_certificate")]
        BirthCertificate,
        [Description("bank_statement")]
        BankStatement,
        [Description("unknown")]
        Unknown
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DocumentSide
    {
        [Description("front")]
        Front, 
        [Description("back")]
        Back
    }

    public class Document
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;

        [JsonProperty("href")]
        public string HRef;

        [JsonProperty("file_name")]
        public string FileName;

        [JsonProperty("file_type")]
        public string FileType;

        [JsonProperty("file_size")]
        public int FileSize;

        [JsonProperty("type")]
        public DocumentType Type;

        [JsonProperty("side")]
        public DocumentSide Side;
    }
}
