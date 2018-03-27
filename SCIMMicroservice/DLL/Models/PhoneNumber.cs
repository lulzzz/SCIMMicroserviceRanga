using System.ComponentModel.DataAnnotations;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.DLL.Models
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }

        public PhoneNumberType Type { get; set; }

        public string Value { get; set; }
    }
}
