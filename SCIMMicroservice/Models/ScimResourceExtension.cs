using ScimMicroservice.Attributes;

namespace ScimMicroservice.Models
{
    public abstract class ScimResourceExtension
    {
        [ScimInternal]
        protected internal abstract string SchemaIdentifier { get; }

        public abstract int CalculateVersion();

    }
}
