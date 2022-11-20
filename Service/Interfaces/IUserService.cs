using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.DTO;
using IsApi.Entities;

namespace IsApi.Service.Interfaces
{
    public interface IUserService
    {
        List<string> GetRolesByUsername(string username) ; 
        void CreateUser(CreateUserRequest request);
        void CreateRole(CreateRoleRequest request);
        void AddRoleToUser(AddRoleRequest request);
        KeyValuePair<bool,string> CheckValidUser(TokenRequest request);

        User FindUserByUsername(string username);
        List<UserDto> FindAllUsers();
    }
}