using System.ComponentModel.DataAnnotations;

namespace ScimMicroservice.DLL.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public int MetaDateId { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string Description { get; set; }

        public string LogonName { get; set; }

        public string Email { get; set; }

        public Meta Meta { get; set; }
    }
}
