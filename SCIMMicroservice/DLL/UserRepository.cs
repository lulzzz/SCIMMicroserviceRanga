using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Exceptions;
using static ScimMicroservice.DLL.Models.Enums;

namespace ScimMicroservice.DLL.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private SCIMContext dbContext;

        public UserRepository(SCIMContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Persists the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user)
        {
            user.Meta = new Meta(ResourceType.User)
            {
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                ResourceType = ResourceType.User,
            };

            dbContext.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Deletes the <see cref="ScimUser"/> resource associated with the specified <paramref name="userId"/>.
        /// Clients request resource removal via DELETE.  Service providers MAY
        /// choose not to permanently delete the resource but MUST return a 404
        /// (Not Found) error code for all operations associated with the
        /// previously deleted resource.Service providers MUST omit the
        /// resource from future query results.In addition, the service
        /// provider SHOULD NOT consider the deleted resource in conflict
        /// calculation.  For example, if a User resource is deleted, a CREATE
        /// request for a User resource with the same userName as the previously
        /// deleted resource SHOULD NOT fail with a 409 error due to userName
        /// conflict.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteUser(int userId)
        {
            var user = await dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the <see cref="ScimUser"/> resource associated with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUser(int userId)
        {
            var user = await dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return user;
        }

        /// <summary>
        /// Returns whether the specified <paramref name="userName"/> is available or already in use.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsUserNameAvailable(string userName)
        {
            var userNameBytes = Encoding.UTF8.GetBytes(userName);

            return dbContext.Users
                .All(u => ((IStructuralEquatable)userNameBytes)
                .Equals(Encoding.UTF8.GetBytes(u.Username), StructuralComparisons.StructuralEqualityComparer));

        }

        /// <summary>
        /// Updates the specified <paramref name="user"/> record.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUser(User user)
        {
            var dbUser = await dbContext.Users.FindAsync(user.Id);

            if (dbUser == null)
            {
                throw new NotFoundException("User not found");
            }
            
            dbUser = user;
            dbUser.Meta.LastModified = DateTime.Now;

            dbContext.Update(dbUser);
            await dbContext.SaveChangesAsync();
            return dbUser;
        }

        /// <summary>
        /// Determines whether a user with the specified <paramref name="userId"/> exists.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> UserExists(string userId)
        {
            var user = await dbContext.Users.FindAsync(userId);

            if(user == null)
            {
                return false;
            }

            return true;
        }
    }
}
