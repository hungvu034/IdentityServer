using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsApi.Common.Model
{
    public class EntityBase<K>
    {
        [KeyAttribute]
        public K Id { get; set; }
    }
}