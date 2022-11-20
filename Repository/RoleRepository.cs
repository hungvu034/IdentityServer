using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Repostory;
using IsApi.Entities;
using IsApi.Persistence;
using IsApi.Repository.Interfaces;

namespace IsApi.Repository
{
    public class RoleRepository : RepositoryBase<Role, Guid, IdentityDbContext>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}