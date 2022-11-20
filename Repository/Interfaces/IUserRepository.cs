using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Repostory.Interfaces;
using IsApi.Entities;

namespace IsApi.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User , Guid>
    {
       void AddRoleByUsername(string username,string roleName);
       List<string> GetRoleByUsername(string username);
    }
}