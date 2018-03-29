using ScimMicroservice.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScimMicroservice.DLL.Interfaces
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Persists the specified <paramref name="group"/>.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<Group> CreateGroup(Group group);

        /// <summary>
        /// Gets the <see cref="Group"/> resource associated with the specified <paramref name="groupId"/>.
        /// </summary>
        /// <param name="groupId">int groupId</param>
        /// <returns></returns>
        Task<Group> GetGroup(int groupId);

        /// <summary>
        /// Updates the specified <paramref name="group"/> record.
        /// </summary>
        /// <param name="group">Group group</param>
        /// <returns></returns>
        Task<Group> UpdateGroup(Group group);

        /// <summary>
        /// Deletes the <see cref="Group"/> resource associated with the specified <paramref name="groupId"/>.
        /// Clients request resource removal via DELETE.  Service providers MAY
        /// choose not to permanently delete the resource but MUST return a 404
        /// (Not Found) error code for all operations associated with the
        /// previously deleted resource.Service providers MUST omit the
        /// resource from future query results.In addition, the service
        /// provider SHOULD NOT consider the deleted resource in conflict
        /// calculation.  For example, if a Group resource is deleted, a CREATE
        /// request for a Group resource with the same groupId as the previously
        /// deleted resource SHOULD NOT fail with a 409 error due to groupId
        /// conflict.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task DeleteGroup(int groupId);

        /// <summary>
        /// Searches for users whose metadata satisfy the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        //Task<IEnumerable<User>> QueryUsers(ScimQueryOptions options);
    }
}
