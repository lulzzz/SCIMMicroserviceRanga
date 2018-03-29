using System;
using System.Collections;
using System.Collections.Generic;
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

        public bool AuthenticateUser(string userName, string password)
        {
            var user = dbContext
                .Users
                .FirstOrDefault(u => u.Username.Equals(userName)
                && u.Password.Equals(password));

            return user != null;
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
        /// Get All users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsers()
        {
            return await dbContext
                .Users
                .ToAsyncEnumerable()
                .ToList();
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
        public bool IsUserNameAvailable(string userName)
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
            try
            {
                var dbUser = dbContext.Users.Find(user.Id);

                if (dbUser == null)
                {
                    throw new NotFoundException("User not found");
                }

                var name = dbContext.Names.Find(dbUser.NameId);

                SetName(user, name);

                dbUser.Meta.LastModified = DateTime.Now;
                dbUser.Meta.ResourceType = ResourceType.User;

                if (user.Active.HasValue)
                {
                    dbUser.Active = user.Active;
                }

                if (user.Emails.Any())
                {
                    dbUser.Emails = user.Emails;
                }

                if(user.MailingAddress != null)
                {
                    dbUser.MailingAddress = user.MailingAddress;
                }

                dbUser.Name = user.Name;

                if (user.PhoneNumbers != null && user.PhoneNumbers.Any())
                {
                    dbUser.PhoneNumbers = user.PhoneNumbers;
                }

                dbUser.Username = user.Username;
                await dbContext.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
        
        private void SetName(User user, Name name)
        {
            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.GivenName))
                name.GivenName = user.Name.GivenName;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.FamilyName))
                name.FamilyName = user.Name.FamilyName;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.MiddleName))
                name.MiddleName = user.Name.MiddleName;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.Formatted))
                name.Formatted = user.Name.Formatted;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.HonorificPrefix))
                name.HonorificPrefix = user.Name.HonorificPrefix;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.HonorificSuffix))
                name.HonorificSuffix = user.Name.HonorificSuffix;

            if (user.Name != null && !string.IsNullOrWhiteSpace(user.Name.MiddleName))
                name.MiddleName = user.Name.MiddleName;
        }
    }
}
