using Newtonsoft.Json;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimGroup : ScimResource
    {
        protected ScimGroup() : base(ResourceType.Group)
        {
            /* 3.3.1.Resource Types
             * When adding a resource to a specific endpoint, the meta attribute
             * "resourceType" SHALL be set by the HTTP service provider to the
             * corresponding resource type for the endpoint.  For example, a POST to
             * the endpoint "/Users" will set "resourceType" to "User", and
             * "/Groups" will set "resourceType" to "Group".
             */
            base.Meta = new ScimMeta(ResourceType.Group);
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("logonName")]
        public string LogonName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
