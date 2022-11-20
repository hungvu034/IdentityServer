using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Repostory.Interfaces;
using IsApi.Entities;

namespace IsApi.Repository.Interfaces
{
    public interface IRoleRepository : IRepositoryBase<Role,Guid>
    {
        
    }
}