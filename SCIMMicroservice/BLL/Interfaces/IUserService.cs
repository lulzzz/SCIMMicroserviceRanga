using ScimMicroservice.DLL.Models;
using ScimMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScimMicroservice.BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User user</param>
        /// <returns>User</returns>
        Task<ScimUser> CreateUser(ScimUser user);

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns>User list</returns>
        Task<List<ScimUser>> GetUsers();

        /// <summary>
        /// Get user by user ID
        /// </summary>
        /// <param name="userId">int userId</param>
        /// <returns></returns>
        Task<ScimUser> GetUser(int userId);

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User user</param>
        /// <returns>User</returns>
        Task<ScimUser> UpdateUser(ScimUser user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">int userId</param>
        Task Delete(int userId);
    }
}
