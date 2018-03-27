using AutoMapper;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.DLL;
using ScimMicroservice.DLL.Models;
using ScimMicroservice.Models;
using System;
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

        public async Task<ScimUser> CreateUser(ScimUser user)
        {
            var usr = mapper.Map<ScimUser, User>(user);
            return mapper.Map<User, ScimUser>(await repository.CreateUser(usr));

        }

        public async Task Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ScimUser> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScimUser>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<ScimUser> UpdateUser(ScimUser user)
        {
            throw new NotImplementedException();
        }
    }
}
