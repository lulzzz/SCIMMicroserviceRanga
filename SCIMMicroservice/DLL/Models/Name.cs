using System.ComponentModel.DataAnnotations;

namespace ScimMicroservice.DLL.Models
{
    public class Name
    {
        [Key]
        public int Id { get; set; }

        public string Formatted { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string MiddleName { get; set; }

        public string HonorificPrefix { get; set; }

        public string HonorificSuffix { get; set; }
    }
}
