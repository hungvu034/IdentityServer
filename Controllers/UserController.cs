using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Service.Identity;
using Microsoft.AspNetCore.Mvc;
using IsApi.DTO;
using Microsoft.AspNetCore.Authorization;
using IsApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IsApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody]CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Ok(allErrors);
            }
            try
            {
                _userService.CreateUser(request);
                return Ok("Success");
            }
            catch
            {
                return Ok("Failure");
            }
        }
        [HttpPost("add-role")]
        [Authorize(Roles = "admin")]
        public IActionResult AddRoleToUser([FromBody]AddRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Ok(allErrors);
            }
            _userService.AddRoleToUser(request);
            return Ok("Success");
        }
        [HttpPost("role-create")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateRole([FromBody]CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Ok(allErrors);
            }
            try
            {
                _userService.CreateRole(request);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("all-users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.FindAllUsers());
        }
    }
}