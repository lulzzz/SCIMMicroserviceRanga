using AutoMapper;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.DLL;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScimMicroservice.BLL
{
    public class UserService : IUserService
    {
        private IUserRepository repository;
        private IMapper mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public bool AuthenticateUser(ScimLogin loginModel)
        {
            return repository
                .AuthenticateUser(
                loginModel.UserName,
                loginModel.Password);
        }

        public async Task<ScimUser> CreateUser(ScimUser user)
        {
            var usr = mapper.Map<ScimUser, User>(user);
            return mapper.Map<User, ScimUser>(await repository.CreateUser(usr));

        }
        public async Task Delete(int userId)
        {
            await repository.DeleteUser(userId);
        }

        public async Task<ScimUser> GetUser(int userId)
        {
            var user = await repository.GetUser(userId);
            return mapper.Map<User, ScimUser>(user);
        }

        public async Task<List<ScimUser>> GetUsers()
        {
            var users = await repository.GetAllUsers();
            return mapper.Map<List<User>, List<ScimUser>>(users);
        }

        public async Task<ScimUser> UpdateUser(ScimUser user)
        {
            var usr = mapper.Map<ScimUser, User>(user);
            return mapper.Map<User, ScimUser>(await repository.UpdateUser(usr));
        }
    }
}
