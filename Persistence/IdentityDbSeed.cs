using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Service.Identity;
using IsApi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IsApi.Persistence
{
    public class IdentityDbSeed
    {
        IdentityDbContext _identityContext;
        IUserService _userService;
        public IdentityDbSeed(IdentityDbContext identityContext, IUserService userService)
        {
            _identityContext = identityContext;
            _userService = userService;
        }
        public void Seed()
        {
            _identityContext.Database.Migrate();
            if (!_identityContext.Users.Any())
            {
                _userService.CreateUser(new DTO.CreateUserRequest()
                {
                    UserName = "admin",
                    Password = "admin",
                    FullName = "admin"
                });
                _userService.CreateRole(new DTO.CreateRoleRequest()
                {
                    RoleName = "admin"
                });
                _userService.CreateRole(new DTO.CreateRoleRequest()
                {
                    RoleName = "member"
                });
                _userService.AddRoleToUser(new DTO.AddRoleRequest()
                {
                    UserName = "admin",
                    RoleName = "admin"
                });

            }
        }
    }
}