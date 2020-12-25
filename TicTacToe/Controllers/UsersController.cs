﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TicTacToe.WebApi.Models;
using TicTacToe.WebApi.Services;

namespace TicTacToe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            if (users is null || !users.Any())
            {
                return NotFound("No records");
            }

            return Ok(users);
        }

        // GET api/<UsersController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task CreateUserAsync([FromBody] UserModel user)
        {
            await _userService.CreateUserAsync(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task UpdateUserAsync(Guid id, [FromBody] UserModel user)
        {
            await _userService.UpdateUserAsync(new UserModel
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthModel model)
        {
            var user = await _userService.Authenticate(model);
            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
