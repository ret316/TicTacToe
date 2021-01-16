using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    /// <summary>
    /// Interface of UserService
    /// </summary>
    public interface IUserService : IAuthentication
    {
        /// <summary>
        /// Method for getting all users
        /// </summary>
        /// <param name="pageNumber">Selected page number</param>
        /// <param name="pageSize">Number of objects in pagination</param>
        /// <returns>Collection of user models</returns>
        Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber, int pageSize);
        /// <summary>
        /// Method for getting user details
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User model</returns>
        Task<User> GetUserAsync(Guid id);
        /// <summary>
        /// Method for saving user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task<bool> CreateUserAsync(User user);
        /// <summary>
        /// Method for updating user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task<bool> UpdateUserAsync(User user);
        /// <summary>
        /// Method for deletion user from database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(Guid id);
    }
}
