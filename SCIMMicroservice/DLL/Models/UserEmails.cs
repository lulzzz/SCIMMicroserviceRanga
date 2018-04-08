using System;
using System.ComponentModel.DataAnnotations;

namespace ScimMicroservice.DLL.Models
{
    public class UserEmails
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EmailId { get; set; }
    }
}
