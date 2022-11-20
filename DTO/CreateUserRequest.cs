using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsApi.DTO
{
    public class CreateUserRequest{
        [RequiredAttribute]
        public string UserName { get; set;}
        [Required]
        public string Password { get; set;}
        [Required]
        public string FullName { get; set;}
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set;}
        [EmailAddress]
        public string Email { get; set;}
        [Phone]
        public string PhoneNumber { get; set;}
    }
}