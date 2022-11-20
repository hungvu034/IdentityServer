using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.DTO;
using IsApi.Entities;
using IsApi.Persistence;

using IsApi.Repository.Interfaces;
using IsApi.Service.Interfaces;

namespace IsApi.Service.Identity
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository ; 
        private IRoleRepository _roleRepository ; 

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void AddRoleToUser(AddRoleRequest request)
        {
            _userRepository.AddRoleByUsername(request.UserName,request.RoleName);
        }

        public List<UserDto> FindAllUsers(){
            var users = _userRepository.FindAll().ToList();
            var result = new List<UserDto>();
            foreach(var user in users){
                UserDto userDto = new UserDto();
                userDto.Username = user.Username ; 
                userDto.FullName = user.FullName ; 
                userDto.PhoneNumber = user.PhoneNumber;
                userDto.Email = user.Email ; 
                userDto.Roles = GetRolesByUsername(user.Username);
                result.Add(userDto);
            }
            return result ; 
        }

        public KeyValuePair<bool,string> CheckValidUser(TokenRequest request)
        {
            var currentUser = _userRepository.FindByCondition(x => x.Username == request.UserName).FirstOrDefault();
            if (currentUser == null)
            {
                return new(false,"Username not exist") ; 
            }
            if (HashPassword(request.Password) != currentUser.PasswordHash)
            {
                return new(false , "Password not correct");
            }
            return new(true , "Success");
        }

        public void CreateRole(CreateRoleRequest request)
        {
            _roleRepository.Create(new Role(){
                Name = request.RoleName
            });
        }

        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var hashPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashPassword);
        }

        public void CreateUser(CreateUserRequest request)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(request.Password);
            var hashPassword = sha.ComputeHash(asByteArray);
            User newUser = new User()
            {
                Username = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Roles = new List<Role>(),
                FullName = request.FullName,
                PasswordHash = Convert.ToBase64String(hashPassword)
            };
            _userRepository.Create(newUser);

        }

        public List<string> GetRolesByUsername(string username)
        => _userRepository.GetRoleByUsername(username);

        public User FindUserByUsername(string username)
        {
            var user = _userRepository.FindByCondition(x => x.Username == username).FirstOrDefault();
            return user;
        }
    }
}