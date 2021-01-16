using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Services
{
    //TODO use as friendly assemble
    /// <summary>
    /// Interface of user service
    /// </summary>
    public interface IUserService 
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
        /// Method for searching user in database
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User model</returns>
        Task<User> GetUserAsync(string email);
        /// <summary>
        /// Method for saving user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task CreateUserAsync(User user);
        /// <summary>
        /// Method for updating user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);
        /// <summary>
        /// Method for deletion user from database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task DeleteUserAsync(User user);
    }
}
