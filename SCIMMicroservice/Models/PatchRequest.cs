using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ScimMicroservice.Models
{
    public class PatchRequest<T> where T : class
    {
        [JsonProperty("schemas")]
        public List<string> Schemas { get; set; }

        [JsonProperty("Operations")]
        public IEnumerable<Operation<T>> Operations { get; set; }
    }
}
