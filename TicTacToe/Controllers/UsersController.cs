using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;
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
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize)
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
        public async Task<IActionResult> GetAsync(Guid id)
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
        public async Task Post([FromBody] UserViewModel user)
        {
            await _userService.CreateUserAsync(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] UserViewModel user)
        {
            await _userService.UpdateUserAsync(new UserViewModel
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
