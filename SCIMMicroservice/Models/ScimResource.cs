using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScimMicroservice.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimResource : SchemaBase
    {
        private ResourceType resourceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        protected ScimResource(ResourceType resourceType)
        {
            Extensions = new ResourceExtensions();
            this.resourceType = resourceType;
        }

        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <value>The schemas.</value>
        public override ISet<string> Schemas
        {
            get
            {
                return new HashSet<string>(
                    new[] { SchemaIdentifier }
                        .Concat(Extensions.Schemas)
                        .ToList());
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier for a SCIM resource as defined by the service provider.
        /// </summary>
        /// <value>The identifier.</value>
        [Description(@"A unique identifier for a SCIM resource as defined by the service provider.")]
        [JsonProperty(Order = -5, PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the resource as defined by the provisioning client.
        /// </summary>
        /// <value>The external identifier.</value>
        [Description(@"An identifier for the resource as defined by the provisioning client.")]
        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the metadata which describes the resource instance.
        /// </summary>
        /// <value>The meta.</value>
        [JsonProperty(Order = 9999, PropertyName = "meta")]
        public ScimMeta Meta { get; set; }

        /// <summary>
        /// Gets the extension collection for the resource instance.
        /// </summary>
        /// <value>The extensions.</value>
        [JsonIgnore]
        [ScimInternal]
        public ResourceExtensions Extensions { get; private set; }

        /// <summary>
        /// This is only used for internal Owin.Scim serialization purposes.
        /// </summary>
        /// <value>The extension serialization.</value>
        [JsonExtensionData]
        [ScimInternal]
        internal IDictionary<string, JToken> ExtensionSerialization { get; set; }

        public override string SchemaIdentifier
        {
            get { return ScimConstants.Schemas[resourceType]; }
        }

        /// <summary>
        /// Gets the resource extension instance for the type <typeparamref name="T"/> specified. 
        /// If no extension exists, it initializes an instance and returns the created instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public T Extension<T>() where T : ScimResourceExtension, new()
        {
            return Extensions.GetOrCreate<T>();
        }

        /// <summary>
        /// Adds the specified <paramref name="extension"/> to the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extension">The extension.</param>
        public void AddExtension<T>(T extension) where T : ScimResourceExtension
        {
            Extensions.Add(extension);
        }

        internal void AddNullExtension(Type extensionType, string extensionSchema)
        {
            Extensions.Add(extensionType, extensionSchema, null);
        }

        public virtual int CalculateVersion()
        {
            return new
            {
                Id,
                ExternalId,
                Extensions = Extensions.CalculateVersion()
            }.GetHashCode();
        }
    }
}
