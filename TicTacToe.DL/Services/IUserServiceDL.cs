using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    /// <summary>
    /// Interface of user service
    /// </summary>
    public interface IUserServiceDL 
    {
        /// <summary>
        /// Method for getting all users
        /// </summary>
        /// <param name="pageNumber">Selected page number</param>
        /// <param name="pageSize">Number of objects in pagination</param>
        /// <returns>Collection of user models</returns>
        Task<IEnumerable<UserDL>> GetAllUsersAsync(int pageNumber, int pageSize);
        /// <summary>
        /// Method for getting user details
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User model</returns>
        Task<UserDL> GetUserAsync(Guid id);
        /// <summary>
        /// Method for searching user in database
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User model</returns>
        Task<UserDL> GetUserAsync(string email);
        /// <summary>
        /// Method for saving user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task CreateUserAsync(UserDL user);
        /// <summary>
        /// Method for updating user in database
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task UpdateUserAsync(UserDL user);
        /// <summary>
        /// Method for deletion user from database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task DeleteUserAsync(UserDL user);
    }
}
