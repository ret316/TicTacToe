using Microsoft.AspNetCore.Mvc;
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
                return NoContent();
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
                return NoContent();
            }

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserModel user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserModel user)
        {
            var result = await _userService.UpdateUserAsync(new UserModel
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthModel model)
        {
            var user = await _userService.Authenticate(model);
            if (user is null)
            {
                return NoContent();
            }

            return Ok(user);
        }
    }
}
