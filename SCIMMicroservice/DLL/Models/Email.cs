using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.DLL.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        public EmailType Type { get; set; }

        public bool Primary { get; set; }

        public string Value { get; set; }
    }
}
