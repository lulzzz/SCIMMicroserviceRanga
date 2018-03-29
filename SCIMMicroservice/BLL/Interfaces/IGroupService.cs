using ScimMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScimMicroservice.BLL.Interfaces
{
    public interface IGroupService
    {
        Task<ScimGroup> CreateGroup(ScimGroup group);
        Task<ScimGroup> GetGroup(int groupId);
        Task<ScimGroup> UpdateGroup(ScimGroup group);
        Task DeleteGroup(int groupId);
    }
}
