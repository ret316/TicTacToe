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

        /// <summary>
        /// Method for getting
        /// <para>GET api/users</para>
        /// </summary>
        /// <param name="pageNumber">Selected page number</param>
        /// <param name="pageSize">Number of objects in pagination</param>
        /// <returns></returns>
        [HttpGet] public async Task<IActionResult> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            if (users is null || !users.Any())
            {
                return NoContent();
            }

            return Ok(users);
        }

        /// <summary>
        /// Method for getting user details
        /// <para>GET api/users/id</para>
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpGet("{id}")] public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user is null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        /// <summary>
        /// Method for user creation
        /// <para>POST api/users</para>
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        [HttpPost] public async Task<IActionResult> CreateUserAsync([FromBody] UserModel user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Method for user updating
        /// <para>PUT api/users/id</para>
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="user">New user model</param>
        /// <returns></returns>
        [HttpPut("{id}")] public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserModel user)
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

        /// <summary>
        /// Method for user deletion
        /// <para>DELETE api/users/id</para>
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Method for user authentication
        /// <para>POST api/users/authenticate</para>
        /// </summary>
        /// <param name="model">User authentication model</param>
        /// <returns></returns>
        [AllowAnonymous] [HttpPost("authenticate")] public async Task<IActionResult> Authenticate([FromBody] UserAuthModel model)
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
