using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Model;

namespace IsApi.Entities
{
    public class User : EntityBase<Guid>
    {
        public string FullName { get; set;}
        public string Username { get; set;}
        public string? Email { get; set;}
        public string? PhoneNumber { get; set;}
        public string PasswordHash { get; set;}
        public List<Role>? Roles { get; set;}
    }
}