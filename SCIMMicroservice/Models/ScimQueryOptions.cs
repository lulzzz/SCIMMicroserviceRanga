using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimQueryOptions : SchemaBase
    {
        public ScimQueryOptions()
        {
            Attributes = new HashSet<string>();
            ExcludedAttributes = new HashSet<string>();
        }

        [JsonProperty("attribues")]
        public ISet<string> Attributes { get; internal set; }

        [JsonProperty("excludedAttributes")]
        public ISet<string> ExcludedAttributes { get; internal set; }

        [JsonProperty("filter")]
        public PathFilterExpression Filter { get; internal set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; internal set; }

        [JsonProperty("sortOrder")]
        public SortOrder SortOrder { get; internal set; }

        [JsonProperty("startIndex")]
        public int StartIndex { get; internal set; }

        [JsonProperty("count")]
        public int Count { get; internal set; }

        public override string SchemaIdentifier
        {
            get { return ScimConstants.Messages.SearchRequest; }
        }
    }
}
