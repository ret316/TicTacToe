using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    public interface IUserServiceDL 
    {
        Task<IEnumerable<UserDL>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserDL> GetUserAsync(Guid id);
        Task<UserDL> GetUserAsync(string email);
        Task CreateUserAsync(UserDL user);
        Task UpdateUserAsync(UserDL user);
        Task DeleteUserAsync(UserDL user);
    }
}
