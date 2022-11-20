using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsApi.DTO
{
    public class AddRoleRequest
    {
        [RequiredAttribute]
        public string UserName { get; set;}
        [Required]
        public string RoleName { get; set;}
    }
}