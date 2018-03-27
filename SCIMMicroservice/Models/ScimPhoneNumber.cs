using Newtonsoft.Json;
using ScimMicroservice.Attributes;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimPhoneNumber : MultiValuedAttribute
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public PhoneNumberType Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
