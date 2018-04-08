using Newtonsoft.Json;
using System;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimMeta
    {
        public ScimMeta(string resourceType)
        {
            this.ResourceType = resourceType;
        }

        public ScimMeta()
        {
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        public string VersionTest
        {
            get
            {
                return this.GetHashCode().ToString();
            }
        }
    }
}
