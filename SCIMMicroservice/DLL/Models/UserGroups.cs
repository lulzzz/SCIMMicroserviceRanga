﻿using System.ComponentModel.DataAnnotations;

namespace ScimMicroservice.DLL.Models
{
    public class UserGroups
    {
        [Key]
        public int Id { get; set; }

        public int MetaDataId { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }

        public virtual Meta Meta { get; set; }

        public virtual User User {get;set;}

        public virtual Group Group { get; set; }
    }
}
