using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Onfido.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DocumentType {
        [EnumMember(Value = "passport")]
        Passport,
        [EnumMember(Value = "national_identity_card")]
        NationalIdentityCard,
        [EnumMember(Value = "work_permit")]
        WorkPermit,
        [EnumMember(Value = "driving_license")]
        DrivingLicense,
        [EnumMember(Value = "national_insurance")]
        NationalInsurance,
        [EnumMember(Value = "birth_certificate")]
        BirthCertificate,
        [EnumMember(Value = "bank_statement")]
        BankStatement,
        [EnumMember(Value = "unknown")]
        Unknown
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DocumentSide
    {
        [EnumMember(Value = "front")]
        Front, 
        [EnumMember(Value = "back")]
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
