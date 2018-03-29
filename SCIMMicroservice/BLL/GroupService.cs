using AutoMapper;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.DLL.Interfaces;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Models;
using System.Threading.Tasks;

namespace ScimMicroservice.BLL
{
    public class GroupService : IGroupService
    {
        private IGroupRepository repository;
        private IMapper mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<ScimGroup> CreateGroup(ScimGroup group)
        {
            var grp = mapper.Map<ScimGroup, Group>(group);
            return mapper.Map<Group, ScimGroup>(await repository.CreateGroup(grp));
        }

        public async Task DeleteGroup(int groupId)
        {
            await repository.DeleteGroup(groupId);
        }

        public async Task<ScimGroup> GetGroup(int groupId)
        {
            return mapper.Map<Group, ScimGroup>(await repository.GetGroup(groupId));
        }

        public async Task<ScimGroup> UpdateGroup(ScimGroup group)
        {
            var grp = mapper.Map<ScimGroup, Group>(group);
            return mapper.Map<Group, ScimGroup>(await repository.UpdateGroup(grp));
        }
    }
}
