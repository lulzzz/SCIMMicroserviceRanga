using Newtonsoft.Json;
using ScimMicroservice.Attributes;
using System.Collections.Generic;

namespace ScimMicroservice.Models
{
    public abstract class SchemaBase
    {
        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <value>The schemas.</value>
        [JsonProperty("schemas", Order = -10)]
        public virtual ISet<string> Schemas
        {
            get
            {
                return new HashSet<string>(new[] { SchemaIdentifier });
            }
        }

        /// <summary>
        /// Gets the schema identifier for the resource instance.
        /// </summary>
        /// <value>The schema identifier.</value>
        [JsonIgnore]
        [ScimInternal]
        public abstract string SchemaIdentifier { get; }
    }
}
