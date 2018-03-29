using ScimMicroservice.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScimMicroservice.DLL
{
    public interface IUserRepository
    {
        /// <summary>
        /// Persists the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> CreateUser(User user);

        /// <summary>
        /// Gets the <see cref="ScimUser"/> resource associated with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUser(int userId);

        /// <summary>
        /// Gets the <see cref="ScimUser"/> resource associated with the specified <paramref name="userId"/>.
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// Updates the specified <paramref name="user"/> record.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> UpdateUser(User user);

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
        Task DeleteUser(int userId);

        /// <summary>
        /// Searches for users whose metadata satisfy the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        //Task<IEnumerable<User>> QueryUsers(ScimQueryOptions options);

        /// <summary>
        /// Returns whether the specified <paramref name="userName"/> is available or already in use.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsUserNameAvailable(string userName);

        /// <summary>
        /// Determines whether a user with the specified <paramref name="userId"/> exists.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> UserExists(string userId);

        /// <summary>
        /// Authenticate user using username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUser(string userName, string password);
    }
}

