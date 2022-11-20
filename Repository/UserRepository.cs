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
    public class UserRepository : RepositoryBase<User, Guid, IdentityDbContext>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {
        }

        public void AddRoleByUsername(string username, string roleName)
        {
            var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("user not exist");
            }
            var role = _context.Roles.Where(x => x.Name == roleName).FirstOrDefault();
            if (role == null)
            {
                throw new Exception("role not exist");
            }
            _context.Entry(user).Collection(x => x.Roles).Load();
            user.Roles.Add(role);
            _context.SaveChanges();
        }

        public List<string> GetRoleByUsername(string username)
        {
            var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("user not exist");
            }
            _context.Entry(user).Collection(p => p.Roles).Load();
            var result = user.Roles.ToList().Select(x => x.Name);
            return result.ToList();
        }
    }
}