﻿using System;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.ViewModels;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<List<UserDetailsViewModel>>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        
        [HttpGet("{id}")]
        [Authorize (Roles = "Admin, User")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var result = await _userService.GetId(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpGet("email/{email}")]
        [Authorize (Roles = "Admin, User")]
        public async Task<ActionResult<UserViewModel>> GetUserByMail(string email)
        {
            var result = await _userService.GetByEmail(email);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var result = await _userService.CreateUser(user);
            return result == null ? Unauthorized("User already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var result = await _userService.UpdateUser(id, user);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }
    }
}