using ScimMicroservice.DLL.Models;
using System;
using System.Collections.Generic;

namespace ScimMicroservice.DLL.Models
{
    public class User
    {
        public User()
        {
            Emails = new List<Email>();
            PhoneNumbers = new List<PhoneNumber>();
            Name = new Name();
            Meta = new Meta(Enums.ResourceType.User);
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public Name Name { get; set; }

        public List<Email> Emails { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public string ExternalId { get; set; }

        public bool Disabled { get; set; }

        public bool Locked { get; set; }

        public DateTime ExpiresOn { get; set; }

        public bool PasswordNeverExpires { get; set; }

        public bool CanNotChangePassword { get; set; }

        public bool MustChangePasswordAtNextLogin { get; set; }

        public DateTime PasswordExpiresOn { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public Meta Meta { get; set; }

        public MailingAddress MailingAddress { get;set;}
    }
}
