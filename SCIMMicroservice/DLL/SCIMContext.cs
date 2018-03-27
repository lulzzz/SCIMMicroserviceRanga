using Microsoft.EntityFrameworkCore;
using ScimMicroservice.DLL.Models;

namespace ScimMicroservice.DLL
{
    public class SCIMContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SCIMContext"/> class
        /// </summary>
        /// <param name="options">options</param>
        public SCIMContext(DbContextOptions<SCIMContext> options)
           : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<UserGroups> UserGroups { get; set; }

    }
}
