using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.DLL.Models
{
    public class Meta
    {
        public Meta() { }

        public Meta(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        [Key]
        public int Id { get; set; }

        public ResourceType ResourceType { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string Location { get; set; }

        public string Version { get; set; }
    }
}
