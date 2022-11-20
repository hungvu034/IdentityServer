using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Model;

namespace IsApi.Entities
{
    public class Role : EntityBase<Guid>
    {
        public string Name { get; set;}
        public List<User>? Users { get; set;}
    }
}