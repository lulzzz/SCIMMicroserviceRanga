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

        public int NameId { get; set; }

        public int MetaId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ExternalId { get; set; }

        public bool? Active { get; set; }

        public virtual Meta Meta { get; set; }

        public virtual MailingAddress MailingAddress { get;set;}

        public virtual Name Name { get; set; }

        public virtual List<Email> Emails { get; set; }

        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
