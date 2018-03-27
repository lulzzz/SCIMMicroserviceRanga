using Newtonsoft.Json;
using ScimMicroservice.Attributes;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimEmail : MultiValuedAttribute
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public EmailType Type { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
