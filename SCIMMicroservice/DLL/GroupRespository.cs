using ScimMicroservice.DLL.Interfaces;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Exceptions;
using System.Threading.Tasks;

namespace ScimMicroservice.DLL
{
    public class GroupRespository : IGroupRepository
    {
        private SCIMContext dbContext;

        public GroupRespository(SCIMContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            this.dbContext.Groups.Add(group);
            await this.dbContext.SaveChangesAsync();
            return group;
        }

        public async Task DeleteGroup(int groupId)
        {
            var group = await dbContext.Groups.FindAsync(groupId);

            if (group == null)
            {
                throw new NotFoundException("Group could not be found");
            }

            dbContext.Groups.Remove(group);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Group> GetGroup(int groupId)
        {
            var group = await dbContext.Groups.FindAsync(groupId);

            if (group == null)
            {
                throw new NotFoundException("Group could not be found");
            }

            return group;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            var dbGroup = dbContext.Groups.Find(group.Id);

            if (dbGroup == null)
            {
                throw new NotFoundException("Group could not be found");
            }

            dbGroup.Description = group.Description == null
                ? dbGroup.Description
                : group.Description;

            dbGroup.DisplayName = group.DisplayName == null
              ? dbGroup.DisplayName
              : group.DisplayName;

            dbGroup.Email = group.Email == null
              ? dbGroup.Email
              : group.Email;

            dbGroup.LogonName = group.LogonName == null
                ? dbGroup.LogonName
                : group.LogonName;

            dbGroup.Name = group.Name == null
               ? dbGroup.Name
               : group.Name;

            dbGroup.Notes = group.Notes == null
              ? dbGroup.Notes
              : group.Notes;

            await dbContext.SaveChangesAsync();
            return group;
        }
    }
}
