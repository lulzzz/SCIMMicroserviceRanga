using Newtonsoft.Json;
using ScimMicroservice.Attributes;
using System;
using System.Collections.Generic;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.Models
{
    public class ScimUser : ScimResource
    {
        protected ScimUser()
        {
            /* 3.3.1.Resource Types
             * When adding a resource to a specific endpoint, the meta attribute
             * "resourceType" SHALL be set by the HTTP service provider to the
             * corresponding resource type for the endpoint.  For example, a POST to
             * the endpoint "/Users" will set "resourceType" to "User", and
             * "/Groups" will set "resourceType" to "Group".
             */
            base.Meta = new ScimMeta(ResourceType.User);
        }

        //[JsonProperty("id")]
        //public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("name")]
        public ScimName Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("profileUrl")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("emails")]
        public IEnumerable<ScimEmail> Emails { get; set; }

        [JsonProperty("phoneNumbers")]
        public IEnumerable<ScimPhoneNumber> PhoneNumbers { get; set; }
       
        [JsonProperty("addresses")]
        public IEnumerable<ScimMailingAddress> Addresses { get; set; }

        public override int CalculateVersion()
        {
            return new
            {
                Base = base.CalculateVersion(),
                Active,
                Locale,
                Name = Name == null ? 0 : Name.CalculateVersion(),
                NickName,
                DisplayName,
                Password,
                PreferredLanguage,
                ProfileUrl,
                Timezone,
                Title,
                UserName,
                UserType,
                Addresses = Addresses.GetMultiValuedAttributeCollectionVersion(),
                Emails = Emails.GetMultiValuedAttributeCollectionVersion(),
               // Entitlements = Entitlements.GetMultiValuedAttributeCollectionVersion(),
               // Groups = Groups.GetMultiValuedAttributeCollectionVersion(),
                //Ims = Ims.GetMultiValuedAttributeCollectionVersion(),
                PhoneNumbers = PhoneNumbers.GetMultiValuedAttributeCollectionVersion(),
               // Photos = Photos.GetMultiValuedAttributeCollectionVersion(),
               // Roles = Roles.GetMultiValuedAttributeCollectionVersion(),
               // X509Certificates = X509Certificates.GetMultiValuedAttributeCollectionVersion()
            }.GetHashCode();
        }
    }
}
